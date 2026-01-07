using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoEstoque.Models;

public class CompraItem
{
    [Key]
    public int CompraItemId { get; set; }
    
    public int CompraId { get; set; }
    
    public int ProdutoId { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal Quantidade { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorUnitario { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorTotal { get; set; }
    
    [ForeignKey("CompraId")]
    public virtual Compra Compra { get; set; } = null!;
    
    [ForeignKey("ProdutoId")]
    public virtual Produto Produto { get; set; } = null!;
    
    public virtual ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = new List<MovimentacaoEstoque>();
}
