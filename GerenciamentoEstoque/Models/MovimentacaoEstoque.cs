using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GerenciamentoEstoque.Enums;

namespace GerenciamentoEstoque.Models;

public class MovimentacaoEstoque
{
    [Key]
    public int MovimentacaoEstoqueId { get; set; }
    
    public int ProdutoId { get; set; }
    
    public TipoMovimentacao TipoMovimentacao { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal Quantidade { get; set; }
    
    public int? CompraItemId { get; set; }
    
    public int? VendaItemId { get; set; }
    
    public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    
    public int UsuarioId { get; set; }
    
    [MaxLength(500)]
    public string? Observacao { get; set; }
    
    [ForeignKey("ProdutoId")]
    public virtual Produto Produto { get; set; } = null!;
    
    [ForeignKey("CompraItemId")]
    public virtual CompraItem? CompraItem { get; set; }
    
    [ForeignKey("VendaItemId")]
    public virtual VendaItem? VendaItem { get; set; }
    
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;
}
