using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("PerfilesUsuarios", Schema = "blog")]
    public class PerfilUsuario
    {
        [Key]//la pk tambien es la FK hacia Usuario
        public int UsuarioId { get; set; }

        [MaxLength(500)]
        public string? Biografia { get; set; }
        [MaxLength(300)]
        public string? FotoUrl { get; set; }

        //Propiedad de navegacion inversa
        public Usuario Usuario { get; set; }
    }
}
