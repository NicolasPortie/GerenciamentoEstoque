using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEstoque.Models;

public class Fornecedor
{
    [Key]
    public int FornecedorId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? Documento { get; set; }
    
    [MaxLength(20)]
    public string? Telefone { get; set; }
    
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [MaxLength(200)]
    public string? Logradouro { get; set; }
    
    [MaxLength(100)]
    public string? Cidade { get; set; }
    
    [MaxLength(2)]
    public string? Estado { get; set; }
    
    [MaxLength(10)]
    public string? Cep { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
