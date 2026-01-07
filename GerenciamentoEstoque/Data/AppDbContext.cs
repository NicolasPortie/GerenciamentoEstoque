using Microsoft.EntityFrameworkCore;
using GerenciamentoEstoque.Models;

namespace GerenciamentoEstoque.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<UnidadeMedida> UnidadesMedida { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Estoque> Estoques { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<UsuarioPerfil> UsuarioPerfis { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraItem> CompraItens { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaItem> VendaItens { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
    public DbSet<Auditoria> Auditorias { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UsuarioPerfil>()
            .HasKey(up => new { up.UsuarioId, up.PerfilId });
        
        modelBuilder.Entity<Estoque>()
            .HasOne(e => e.Produto)
            .WithOne(p => p.Estoque)
            .HasForeignKey<Estoque>(e => e.ProdutoId);
        
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Produto>()
            .HasIndex(p => p.CodigoBarras);
        
        modelBuilder.Entity<Produto>()
            .HasIndex(p => p.CodigoInterno);
    }
}
