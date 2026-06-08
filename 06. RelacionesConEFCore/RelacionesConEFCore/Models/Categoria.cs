using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Categorias", Schema = "blog")]
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La categoría es obligatoria"), MaxLength(80)]
        public string Nombre { get; set; }

        // Relación 1:N (una categoría tiene muchos artículos)
        public List<Articulo> Articulos { get; set; }
    }
}
