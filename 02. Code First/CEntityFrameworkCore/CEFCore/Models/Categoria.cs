

namespace CEFCore.Models
{
    public class Categoria
    {
        //Por convencion al poner un entero con el nombre Id, sabe que es un valor autoincrementable, identity y primarykey
        //Para poner otro nombre, se tiene que usar DataAnotetion [key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}