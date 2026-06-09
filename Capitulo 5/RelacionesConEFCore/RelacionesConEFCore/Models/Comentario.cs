using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Comentarios", Schema = "blog")]
    public class Comentario

    {
        public int Id { get; set; }
        public string Texto { get; set; } = null;

        //fk Relacion 1-1
        public int UsuarioId { get; set; }


        //Propiedad de navegacion
        public Usuario Usuario { get; set; } = null;

        // Relacion con articulo (1:N)
        [ForeignKey(nameof(Articulo))]
        public int ArticuloId { get; set; }

        public Articulo Articulo { get; set; }
    }
}
