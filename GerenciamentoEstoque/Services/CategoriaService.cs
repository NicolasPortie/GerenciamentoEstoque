using Microsoft.EntityFrameworkCore;
using GerenciamentoEstoque.Data;
using GerenciamentoEstoque.Models;

namespace GerenciamentoEstoque.Services;

public interface ICategoriaService
{
    Task<List<Categoria>> ObterTodasAsync();
    Task<List<Categoria>> ObterAtivasAsync();
    Task<Categoria?> ObterPorIdAsync(int id);
    Task<Categoria> CriarAsync(Categoria categoria);
    Task<Categoria> AtualizarAsync(Categoria categoria);
    Task<bool> ExcluirAsync(int id);
    Task<bool> PossuiProdutosVinculadosAsync(int id);
}

public class CategoriaService : ICategoriaService
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public CategoriaService(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Categoria>> ObterTodasAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Categorias
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<List<Categoria>> ObterAtivasAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Categorias
            .Where(c => c.Ativo)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<Categoria?> ObterPorIdAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Categorias.FindAsync(id);
    }

    public async Task<Categoria> CriarAsync(Categoria categoria)
    {
        using var context = _dbFactory.CreateDbContext();
        
        categoria.DataCadastro = DateTime.Now;
        categoria.Ativo = true;
        
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();
        
        return categoria;
    }

    public async Task<Categoria> AtualizarAsync(Categoria categoria)
    {
        using var context = _dbFactory.CreateDbContext();
        
        context.Entry(categoria).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        return categoria;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        
        var categoria = await context.Categorias.FindAsync(id);
        if (categoria == null)
            return false;

        context.Categorias.Remove(categoria);
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> PossuiProdutosVinculadosAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos.AnyAsync(p => p.CategoriaId == id);
    }
}
