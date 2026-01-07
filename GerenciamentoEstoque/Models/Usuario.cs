using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEstoque.Models;

public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(256)]
    public string SenhaHash { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; } = new List<UsuarioPerfil>();
    
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    
    public virtual ICollection<Venda> Vendas { get; set; } = new List<Venda>();
    
    public virtual ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = new List<MovimentacaoEstoque>();
    
    public virtual ICollection<Auditoria> Auditorias { get; set; } = new List<Auditoria>();
}
