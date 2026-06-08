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

        // Convención: CategoriaId es FK hacia Categoria.Id
        [ForeignKey(nameof(Categoria))] // se refiere a la navegación
        public int CategoriaId { get; set; }

        // Propiedad de navegación
        public Categoria Categoria {  get; set; }


        // Relación N–N: un artículo tiene muchas etiquetas
        public List<Etiqueta> Etiquetas { get; set; }

        // 🔹 Relación 1:N con Comentarios
        public List<Comentario> Comentarios { get; set; }

        //En Base de datos se creara los siguiente:
        /*
         * CREATE TABLE Articulos (
            Id int PRIMARY KEY,
            Titulo nvarchar(200) NOT NULL,
            Contenido nvarchar(max) NOT NULL,
            CategoriaId int NOT NULL,
            FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id)
        );
        */
    }
}
