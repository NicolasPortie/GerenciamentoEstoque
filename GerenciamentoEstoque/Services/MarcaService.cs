using Microsoft.EntityFrameworkCore;
using GerenciamentoEstoque.Data;
using GerenciamentoEstoque.Models;

namespace GerenciamentoEstoque.Services;

public interface IMarcaService
{
    Task<List<Marca>> ObterTodasAsync();
    Task<List<Marca>> ObterAtivasAsync();
    Task<Marca?> ObterPorIdAsync(int id);
    Task<Marca> CriarAsync(Marca marca);
    Task<Marca> AtualizarAsync(Marca marca);
    Task<bool> ExcluirAsync(int id);
    Task<bool> PossuiProdutosVinculadosAsync(int id);
}

public class MarcaService : IMarcaService
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public MarcaService(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Marca>> ObterTodasAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Marcas
            .OrderBy(m => m.Nome)
            .ToListAsync();
    }

    public async Task<List<Marca>> ObterAtivasAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Marcas
            .Where(m => m.Ativo)
            .OrderBy(m => m.Nome)
            .ToListAsync();
    }

    public async Task<Marca?> ObterPorIdAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Marcas.FindAsync(id);
    }

    public async Task<Marca> CriarAsync(Marca marca)
    {
        using var context = _dbFactory.CreateDbContext();
        
        marca.DataCadastro = DateTime.Now;
        marca.Ativo = true;
        
        context.Marcas.Add(marca);
        await context.SaveChangesAsync();
        
        return marca;
    }

    public async Task<Marca> AtualizarAsync(Marca marca)
    {
        using var context = _dbFactory.CreateDbContext();
        
        context.Entry(marca).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        return marca;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        
        var marca = await context.Marcas.FindAsync(id);
        if (marca == null)
            return false;

        context.Marcas.Remove(marca);
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> PossuiProdutosVinculadosAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        return await context.Produtos.AnyAsync(p => p.MarcaId == id);
    }
}
