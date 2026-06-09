using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace RelacionesConEFCore.Models
{
    [Table("Etiquetas", Schema = "blog")]
    public class Etiqueta
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; }
        
        // Relacion N-N una etiqueta puede pertenecer a muchos articulos
        public List<Articulo> Articulos { get; set; }
    }
}
