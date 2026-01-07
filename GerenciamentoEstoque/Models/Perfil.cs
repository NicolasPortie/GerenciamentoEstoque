using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEstoque.Models;

public class Perfil
{
    [Key]
    public int PerfilId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nome { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
    
    public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; } = new List<UsuarioPerfil>();
}
