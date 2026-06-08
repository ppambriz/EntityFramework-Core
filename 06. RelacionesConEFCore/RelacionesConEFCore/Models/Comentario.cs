using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Comentarios", Schema = "blog")]
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; } = null;

        // FK hacia Usuario
        [ForeignKey(nameof(Usuario))] // se refiere a la navegación
        public int UsuarioId { get; set; }

        // Propiedad de navegación
        public Usuario Usuario { get; set; }

        public bool Aprobado { get; set; }

        // 🔹 Relación con Articulo (1:N)
        [ForeignKey(nameof(Articulo))]
        public int ArticuloId { get; set; }

        public Articulo Articulo { get; set; }

    }
}
