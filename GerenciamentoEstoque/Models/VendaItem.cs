using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoEstoque.Models;

public class VendaItem
{
    [Key]
    public int VendaItemId { get; set; }
    
    public int VendaId { get; set; }
    
    public int ProdutoId { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal Quantidade { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorUnitario { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorTotal { get; set; }
    
    [ForeignKey("VendaId")]
    public virtual Venda Venda { get; set; } = null!;
    
    [ForeignKey("ProdutoId")]
    public virtual Produto Produto { get; set; } = null!;
    
    public virtual ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = new List<MovimentacaoEstoque>();
}
