using System.Security.Claims;
using GerenciamentoEstoque.Services;
using GerenciamentoEstoque.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoEstoque.Controllers;

[Route("[controller]")]
public class ContaController : Controller
{
    private readonly IAutenticacaoService _autenticacaoService;

    public ContaController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            return Redirect("/login?erro=DadosInvalidos");
        }

        var usuario = await _autenticacaoService.ValidarUsuarioAsync(modelo.Email, modelo.Senha);
        if (usuario == null)
        {
            return Redirect("/login?erro=CredenciaisInvalidas");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
            new Claim("NomeCompleto", usuario.Nome)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var propriedadesAuth = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            propriedadesAuth);

        return Redirect("/");
    }

    [HttpGet("Sair")]
    public async Task<IActionResult> Sair()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/login");
    }
}
