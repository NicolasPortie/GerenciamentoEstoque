using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEstoque.Models;

public class UnidadeMedida
{
    [Key]
    public int UnidadeMedidaId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10)]
    public string Sigla { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
