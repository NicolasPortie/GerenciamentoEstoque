using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GerenciamentoEstoque.Enums;

namespace GerenciamentoEstoque.Models;

public class Venda
{
    [Key]
    public int VendaId { get; set; }
    
    public int ClienteId { get; set; }
    
    public int UsuarioId { get; set; }
    
    public DateTime DataVenda { get; set; } = DateTime.Now;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorTotal { get; set; }
    
    public StatusOperacao Status { get; set; } = StatusOperacao.Pendente;
    
    public FormaPagamento FormaPagamento { get; set; }
    
    [MaxLength(500)]
    public string? Observacao { get; set; }
    
    [ForeignKey("ClienteId")]
    public virtual Cliente Cliente { get; set; } = null!;
    
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;
    
    public virtual ICollection<VendaItem> Itens { get; set; } = new List<VendaItem>();
}
