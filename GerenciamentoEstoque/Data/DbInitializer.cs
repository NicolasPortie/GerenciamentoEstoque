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

        if (context.Usuarios.Any())
        {
            return;   // Banco já foi semeado
        }

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

        // Dados Iniciais
        
        // Categorias
        var categorias = new List<Categoria>
        {
            new Categoria { Nome = "Eletrônicos" },
            new Categoria { Nome = "Informática" },
            new Categoria { Nome = "Móveis" },
            new Categoria { Nome = "Papelaria" }
        };
        context.Categorias.AddRange(categorias);

        // Marcas
        var marcas = new List<Marca>
        {
            new Marca { Nome = "Samsung" },
            new Marca { Nome = "Dell" },
            new Marca { Nome = "Logitech" },
            new Marca { Nome = "Genérica" }
        };
        context.Marcas.AddRange(marcas);

        // Unidades de Medida
        var unidades = new List<UnidadeMedida>
        {
            new UnidadeMedida { Nome = "Unidade", Sigla = "UN" },
            new UnidadeMedida { Nome = "Caixa", Sigla = "CX" },
            new UnidadeMedida { Nome = "Pacote", Sigla = "PCT" }
        };
        context.UnidadesMedida.AddRange(unidades);

        await context.SaveChangesAsync();

        // Produtos
        var p1 = new Produto
        {
            Nome = "Monitor UltraWide 29\"",
            Descricao = "Monitor Gamer 29 polegadas",
            CodigoInterno = "MON-001",
            CodigoBarras = "7891234567890",
            CategoriaId = categorias[1].CategoriaId, // Informática
            MarcaId = marcas[1].MarcaId, // Dell
            UnidadeMedidaId = unidades[0].UnidadeMedidaId, // UN
            PrecoCusto = 1200.00m,
            PrecoVenda = 1890.00m,
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        var p2 = new Produto
        {
            Nome = "Mouse Sem Fio MX Master",
            Descricao = "Mouse profissional ergonômico",
            CodigoInterno = "MOU-002",
            CodigoBarras = "7899876543210",
            CategoriaId = categorias[0].CategoriaId, // Eletrônicos
            MarcaId = marcas[2].MarcaId, // Logitech
            UnidadeMedidaId = unidades[0].UnidadeMedidaId, // UN
            PrecoCusto = 350.00m,
            PrecoVenda = 599.00m,
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        context.Produtos.AddRange(p1, p2);
        await context.SaveChangesAsync();

        // Estoque Inicial
        context.Estoques.AddRange(
            new Estoque { ProdutoId = p1.ProdutoId, QuantidadeAtual = 15, QuantidadeMinima = 5 },
            new Estoque { ProdutoId = p2.ProdutoId, QuantidadeAtual = 3, QuantidadeMinima = 10 } // Estoque baixo
        );

        await context.SaveChangesAsync();
    }
}
