using System.Security.Cryptography;
using System.Text;
using GerenciamentoEstoque.Data;
using GerenciamentoEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEstoque.Services;

public interface IAutenticacaoService
{
    Task<Usuario?> ValidarUsuarioAsync(string email, string senha);
    string GerarHashSenha(string senha);
}

public class AutenticacaoService : IAutenticacaoService
{
    private readonly AppDbContext _context;

    public AutenticacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ValidarUsuarioAsync(string email, string senha)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null)
            return null;

        if (!usuario.Ativo)
            return null;

        var senhaHashInput = GerarHashSenha(senha);

        if (senhaHashInput == usuario.SenhaHash)
        {
            return usuario;
        }

        return null;
    }

    public string GerarHashSenha(string senha)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
