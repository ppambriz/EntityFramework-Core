using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Usuarios", Schema = "blog")]
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio"), MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio"), EmailAddress, MaxLength(150)]
        public string Email { get; set; }

        // Relación 1–1
        public PerfilUsuario Perfil { get; set; } = null;

        // Relación 1:N -> Un usuario puede escribir muchos comentarios
        public List<Comentario> Comentarios { get; set; }
    }
}
