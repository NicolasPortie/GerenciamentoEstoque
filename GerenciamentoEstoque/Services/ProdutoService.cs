using Microsoft.EntityFrameworkCore;
using GerenciamentoEstoque.Data;
using GerenciamentoEstoque.Models;
using GerenciamentoEstoque.Enums;

namespace GerenciamentoEstoque.Services;

public interface IProdutoService
{
    Task<List<Produto>> ObterTodosAsync();
    Task<List<Produto>> ObterTodosComDetalhesAsync();
    Task<Produto?> ObterPorIdAsync(int id);
    Task<Produto?> ObterPorIdComEstoqueAsync(int id);
    Task<Produto> CriarAsync(Produto produto, decimal qtdInicial, decimal qtdMinima, int usuarioId);
    Task<Produto> AtualizarAsync(Produto produto);
    Task<bool> ExcluirAsync(int id);
}

public class ProdutoService : IProdutoService
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public ProdutoService(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Produto>> ObterTodosAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos
            .OrderByDescending(p => p.DataCadastro)
            .ToListAsync();
    }

    public async Task<List<Produto>> ObterTodosComDetalhesAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Marca)
            .Include(p => p.UnidadeMedida)
            .Include(p => p.Estoque)
            .OrderByDescending(p => p.DataCadastro)
            .ToListAsync();
    }

    public async Task<Produto?> ObterPorIdAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos.FindAsync(id);
    }

    public async Task<Produto?> ObterPorIdComEstoqueAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos
            .Include(p => p.Estoque)
            .FirstOrDefaultAsync(p => p.ProdutoId == id);
    }

    public async Task<Produto> CriarAsync(Produto produto, decimal qtdInicial, decimal qtdMinima, int usuarioId)
    {
        using var context = _dbFactory.CreateDbContext();

        produto.DataCadastro = DateTime.Now;
        produto.Ativo = true;
        
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        if (string.IsNullOrWhiteSpace(produto.CodigoInterno))
        {
            produto.CodigoInterno = "PRD-" + produto.ProdutoId.ToString().PadLeft(5, '0');
            await context.SaveChangesAsync();
        }

        var estoque = new Estoque
        {
            ProdutoId = produto.ProdutoId,
            QuantidadeAtual = qtdInicial,
            QuantidadeMinima = qtdMinima,
            DataAtualizacao = DateTime.Now
        };
        context.Estoques.Add(estoque);

        if (qtdInicial > 0)
        {
            var mov = new MovimentacaoEstoque
            {
                ProdutoId = produto.ProdutoId,
                TipoMovimentacao = TipoMovimentacao.Entrada,
                Quantidade = qtdInicial,
                DataMovimentacao = DateTime.Now,
                UsuarioId = usuarioId,
                Observacao = "Saldo inicial no cadastro"
            };
            context.MovimentacoesEstoque.Add(mov);
        }

        await context.SaveChangesAsync();
        
        return produto;
    }

    public async Task<Produto> AtualizarAsync(Produto produto)
    {
        using var context = _dbFactory.CreateDbContext();
        
        context.Produtos.Update(produto);
        await context.SaveChangesAsync();
        
        return produto;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        
        var produto = await context.Produtos.FindAsync(id);
        if (produto == null)
            return false;

        context.Produtos.Remove(produto);
        await context.SaveChangesAsync();
        
        return true;
    }
}
