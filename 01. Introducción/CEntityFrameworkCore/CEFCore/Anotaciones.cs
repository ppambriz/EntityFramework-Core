

public class Articulo
{
    public int Articulo_Id { get; set; }
    public string TituloArticulo { get; set; }
    public string Descripcion { get; set; }
    
    public double Calificacion { get; set; }
    public DateTime Fecha { get; set; }

//Relacion a otra tabla: Foreing Key
    public int Categoria_Id { get; set; }
    //public Categoria Categoria { get; set; }
}