using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEstoque.Models;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
