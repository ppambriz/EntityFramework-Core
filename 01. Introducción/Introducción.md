#### **¿Qué es un ORM?**

**(Object to Relational Mapping)**



Es una herramienta que permite mapear objetos de C# a tablas de base de datos automáticamente. En lugar de escribir SQL manualmente se trabajan con clases y objetos. **Entity Framework Core** (EF Core) es el ORM más usado en .NET.



//Definicion de la clase C# (modelo)
public class Producto
{
public int Id { get; set; }
public string Nombre { get; set; }
public decimal Precio { gte; set; }
}



//Uso de EF Core para insertar datos
using (var context = new AppDbContext())
{
context.Productos.Add(new Producto { Nombre = "Mouse", Precio = 50});
context.SaveChanges();
}

Sin escribir una sola línea de SQL, EF Core se encarga de generar el INSERT.







##### **Object to Relational Mapping**



**OBJECT**

Es una clase que tenemos en nuestro lenguaje preferido.

**Relational**

Es una base de datos relacional.

**Mapping**

Esta es la parte que interactúa con las dos anteriores es decir entre los objetos y las bases de datos.





##### **Desventajas y consideraciones**

**Curva de aprendizaje inicial:** Requiere entender cómo funciona el mapeo.

**Rendimiento:** Para consultas muy complejas, a veces es mejor usar SQL puro.

**Consultas innecesarias:** Si no se configura bien, EF Core puede generar demasiadas llamadas a la base de datos.

**Abstracción excesiva: Algunos desarrolladores pierden control sobre lo que realmente ocurre.**



Usar el 80% para operación CRUD. Usar SQL puro en consultas pesadas.







##### **Ejemplo práctico**



public class Producto
{
public int Id { get; Set: }
public string Nombre { get; Set: }
}



//Registrar productos
context.Productos.Add(new Producto { Nombre = "Laptop", Precio = 2500});
context.SaveChanges();



//Listar Productos
var lista = context.Productos.Tolist();
foreach (var p in lista)
{

Console.WriteLine($"{p.Nombre"} - ${p.Precio});

}





