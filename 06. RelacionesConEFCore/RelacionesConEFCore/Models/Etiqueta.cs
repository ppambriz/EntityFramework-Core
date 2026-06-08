using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models
{
    [Table("Etiquetas", Schema = "blog")]
    public class Etiqueta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la etiqueta es obligatorio"), MaxLength(50)]
        public string Nombre { get; set; }

        // Relación N–N: una etiqueta puede pertenecer a muchos artículos
        public List<Articulo> Articulos { get; set; }
    }
}
