using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoEstoque.Models;

public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Descricao { get; set; }
    
    [MaxLength(50)]
    public string? CodigoInterno { get; set; }
    
    [MaxLength(50)]
    public string? CodigoBarras { get; set; }
    
    public int CategoriaId { get; set; }
    
    public int MarcaId { get; set; }
    
    public int UnidadeMedidaId { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoCusto { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoVenda { get; set; }
    
    public byte[]? Imagem { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    [ForeignKey("CategoriaId")]
    public virtual Categoria Categoria { get; set; } = null!;
    
    [ForeignKey("MarcaId")]
    public virtual Marca Marca { get; set; } = null!;
    
    [ForeignKey("UnidadeMedidaId")]
    public virtual UnidadeMedida UnidadeMedida { get; set; } = null!;
    
    public virtual Estoque? Estoque { get; set; }
    
    public virtual ICollection<CompraItem> CompraItens { get; set; } = new List<CompraItem>();
    
    public virtual ICollection<VendaItem> VendaItens { get; set; } = new List<VendaItem>();
    
    public virtual ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = new List<MovimentacaoEstoque>();
}
