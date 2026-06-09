using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Usuarios", Schema = "blog")]
    public class Usuario
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }

        //Relacion 1-1
        public PerfilUsuario Perfil { get; set; } = null;

        //Relacion 1:N -> Un usuario puede escribir muchos comentarios
        public List<Comentario> Comentarios { get; set; }
    }
}
