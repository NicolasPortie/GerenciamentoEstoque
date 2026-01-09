using GerenciamentoEstoque.Data;
using GerenciamentoEstoque.Models;
using GerenciamentoEstoque.Services;

namespace GerenciamentoEstoque.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context, IAutenticacaoService authService)
    {
        // Garante que o banco de dados foi criado
        // context.Database.EnsureCreated(); // Cuidado se estiver usando Migrations

        if (!context.Usuarios.Any())
        {
            var senhaHash = authService.GerarHashSenha("123456");

            var admin = new Usuario
            {
                Nome = "Administrador",
                Email = "admin@admin.com",
                SenhaHash = senhaHash,
                Ativo = true,
                DataCadastro = DateTime.Now
            };

            context.Usuarios.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
