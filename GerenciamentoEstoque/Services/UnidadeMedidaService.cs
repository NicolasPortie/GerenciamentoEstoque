using Microsoft.EntityFrameworkCore;
using GerenciamentoEstoque.Data;
using GerenciamentoEstoque.Models;

namespace GerenciamentoEstoque.Services;

public interface IUnidadeMedidaService
{
    Task<List<UnidadeMedida>> ObterTodasAsync();
    Task<List<UnidadeMedida>> ObterAtivasAsync();
    Task<UnidadeMedida?> ObterPorIdAsync(int id);
    Task<UnidadeMedida> CriarAsync(UnidadeMedida unidade);
    Task<UnidadeMedida> AtualizarAsync(UnidadeMedida unidade);
    Task<bool> ExcluirAsync(int id);
    Task<bool> PossuiProdutosVinculadosAsync(int id);
}

public class UnidadeMedidaService : IUnidadeMedidaService
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public UnidadeMedidaService(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<UnidadeMedida>> ObterTodasAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.UnidadesMedida
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<List<UnidadeMedida>> ObterAtivasAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.UnidadesMedida
            .Where(u => u.Ativo)
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<UnidadeMedida?> ObterPorIdAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.UnidadesMedida.FindAsync(id);
    }

    public async Task<UnidadeMedida> CriarAsync(UnidadeMedida unidade)
    {
        using var context = _dbFactory.CreateDbContext();
        
        unidade.DataCadastro = DateTime.Now;
        unidade.Ativo = true;
        unidade.Sigla = (unidade.Sigla ?? string.Empty).Trim().ToUpperInvariant();
        
        context.UnidadesMedida.Add(unidade);
        await context.SaveChangesAsync();
        
        return unidade;
    }

    public async Task<UnidadeMedida> AtualizarAsync(UnidadeMedida unidade)
    {
        using var context = _dbFactory.CreateDbContext();
        
        unidade.Sigla = (unidade.Sigla ?? string.Empty).Trim().ToUpperInvariant();
        context.Entry(unidade).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        return unidade;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        
        var unidade = await context.UnidadesMedida.FindAsync(id);
        if (unidade == null)
            return false;

        context.UnidadesMedida.Remove(unidade);
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> PossuiProdutosVinculadosAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos.AnyAsync(p => p.UnidadeMedidaId == id);
    }
}
