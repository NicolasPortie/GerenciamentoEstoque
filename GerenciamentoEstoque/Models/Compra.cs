using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GerenciamentoEstoque.Enums;

namespace GerenciamentoEstoque.Models;

public class Compra
{
    [Key]
    public int CompraId { get; set; }
    
    public int FornecedorId { get; set; }
    
    public int UsuarioId { get; set; }
    
    public DateTime DataCompra { get; set; } = DateTime.Now;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorTotal { get; set; }
    
    public StatusOperacao Status { get; set; } = StatusOperacao.Pendente;
    
    public FormaPagamento FormaPagamento { get; set; }
    
    [MaxLength(500)]
    public string? Observacao { get; set; }
    
    [ForeignKey("FornecedorId")]
    public virtual Fornecedor Fornecedor { get; set; } = null!;
    
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;
    
    public virtual ICollection<CompraItem> Itens { get; set; } = new List<CompraItem>();
}
