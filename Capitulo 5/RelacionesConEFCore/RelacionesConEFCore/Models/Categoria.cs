using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace RelacionesConEFCore.Models
{
    [Table("Categorias", Schema = "blog")]//El nombre de las tablas se escriben en plural, y el modelo en singular
    public class Categoria
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Nombre { get; set; }

        //Relacion 1:N (una categoria tiene muchos articulos)
        public List<Articulo> Articulos { get; set; }
    }
}
