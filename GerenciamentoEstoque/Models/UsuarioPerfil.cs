using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoEstoque.Models;

public class UsuarioPerfil
{
    public int UsuarioId { get; set; }
    
    public int PerfilId { get; set; }
    
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;
    
    [ForeignKey("PerfilId")]
    public virtual Perfil Perfil { get; set; } = null!;
}
