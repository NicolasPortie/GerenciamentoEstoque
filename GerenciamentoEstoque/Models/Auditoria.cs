using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GerenciamentoEstoque.Enums;

namespace GerenciamentoEstoque.Models;

public class Auditoria
{
    [Key]
    public int AuditoriaId { get; set; }
    
    public int UsuarioId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string NomeTabela { get; set; } = string.Empty;
    
    public TipoAcao TipoAcao { get; set; }
    
    [MaxLength(50)]
    public string ChaveRegistro { get; set; } = string.Empty;
    
    public DateTime DataAcao { get; set; } = DateTime.Now;
    
    public string? DadosAntes { get; set; }
    
    public string? DadosDepois { get; set; }
    
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;
}
