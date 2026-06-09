using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Articulos", Schema = "blog")]
    public class Articulo
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Titulo { get; set; }
        [Required]
        public string Contenido { get; set; }

        public int CategoriaId { get; set; }//Por convencion CategoriaId es FK hacia Categoria.Id

        //Propiedades de navegacion
        public Categoria Categoria { get; set; }

        //Relacion N-N un articulo tiene muchas etiquetas
        public List<Etiqueta> Etiquetas { get; set; }

        //Relacion 1:N con comentarios
        public List<Comentario> Comentarios { get; set; }
    }
}
