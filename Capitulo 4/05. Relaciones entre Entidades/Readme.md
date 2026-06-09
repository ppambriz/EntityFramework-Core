# **INTRODUCCIÓN A LAS RELACIONES ENTRE ENTIDADES**



#### **Tipos de relaciones**

1. Relación uno a uno (1-1)

   * Es un registro en una tabla que se asocia con exactamente un registro en otra.
   * Usuario <---> PerfilUsuario: Cada usuario tiene su perfil único.
2. Relación uno a muchos (1-N)

   * Un registro en una tabla se asocia con muchos registros en otra.
   * Categoría <---> Artículos: Una categoría puede tener muchos artículos, pero cada artículo pertenece a una sola categoría.
3. Relación muchos a muchos (N-N)

   * Un registro en una tabla se asocia con muchos registros en otra y viceversa.
   * Articulo <---> Etiqueta: Un artículo puede tener varias etiquetas, y una etiqueta puede estar en varios artículos.



## 

#### **¿Cómo EF Core detecta automáticamente las relaciones?**

* EF Core usa **convenciones**  para deducir las relaciones sin necesidad de configurarlas manualmente.
* Entre las más comunes están:

  * **Propiedad de navegación + NombreId**

    * Si una entidad tiene una propiedad de navegación hacia otra, y además una propiedad con el nombre de esas entidad + id, EF Core detecta la FK.
    * public class Articulo
    * {

      * public int ID {get; set;}
      * public string Titulo {get; set;} =  null!;
      * 
      * //Propiedad reconocida por convención
      * public int CategoriaID {get; set;}
      * 
      * //Propiedad de navegación
      * public Categoria Categoria {get; set;} = null!;

&#x09;}



* Se podría decir que se les da dos pistas para que detecte **por convención** la fk.







#### **Colecciones**

* Si una entidad tiene una colección, EF Core asume una relación 1-N (Uno a muchos).		
* 
public class Categoria
{
	public int Id {get; set;}
	public string Nombre {get; set;} = null!;

	//Relación 1:N deducida automáticamente
	public List<Articulo> Articulos {get; set;} = new();
}





