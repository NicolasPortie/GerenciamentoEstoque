# Controle de Estoque

Sistema de gestão de estoque e operações comerciais desenvolvido em .NET 9 (Blazor Server). O projeto foca em uma arquitetura limpa e escalável, utilizando Entity Framework Core com SQL Server.

## Tecnologias

*   .NET 9 (Blazor Server)
*   Entity Framework Core 9
*   SQL Server
*   Bootstrap 5

## Configuração e Execução

### Pré-requisitos
*   .NET SDK 9.0+
*   SQL Server (LocalDB ou instância dedicada)

### Instalação

1.  Clone o repositório.
2.  Configure a string de conexão no `appsettings.json`.
3.  Aplique as migrações para criar o banco de dados:

```powershell
dotnet ef database update
```

4.  Execute a aplicação:

```powershell
dotnet run
```

## Estrutura do Banco de Dados

A modelagem foi pensada para garantir integridade e rastreabilidade. Principais entidades:

*   **Cadastros Base**: Produtos, Categorias, Marcas, Unidades de Medida.
*   **Entidades Comerciais**: Clientes e Fornecedores (com endereçamento completo).
*   **Estoque**:
    *   Tabela `Estoque` 1:1 com Produto (controle de saldo atual e mínimo).
    *   Tabela `MovimentacaoEstoque` (Log de todas as entradas, saídas e ajustes).
*   **Operações**: Compras e Vendas com seus respectivos itens.
*   **Segurança e Auditoria**: Controle de usuários e log de alterações em tabelas sensíveis.

## Status do Desenvolvimento

O backend (Models, Contexto e Migrations) está finalizado e validado.

- [x] Modelagem de Dados
- [x] Configuração EF Core e Contexto
- [x] Migrations Iniciais
- [ ] Implementação de Services/Repositories
- [ ] Interfaces CRUD (Blazor Components)
- [ ] Autenticação e Autorização
