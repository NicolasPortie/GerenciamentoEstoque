using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoEstoque.Models;

public class Estoque
{
    [Key]
    [ForeignKey("Produto")]
    public int ProdutoId { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal QuantidadeAtual { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal QuantidadeMinima { get; set; }
    
    public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    
    public virtual Produto Produto { get; set; } = null!;
}
