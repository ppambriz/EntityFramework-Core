using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("PerfilesUsuarios", Schema = "blog")]
    public class PerfilUsuario
    {
        [Key]// la PK también es FK hacia Usuario
        [ForeignKey(nameof(Usuario))]// indica FK hacia la navegación Usuario
        public int UsuarioId { get; set; }

        [MaxLength(500)]
        public string? Biografia { get; set; }
        [MaxLength(300)]
        public string? FotoUrl { get; set; }

        // Propiedad de navegación inversa
        public Usuario Usuario { get; set; } = null;
    }
}
