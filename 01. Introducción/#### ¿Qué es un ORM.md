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



#### **Representación de una consulta OMR en EF**
var productosCaros = context.Productos
                            .where(p => p.Precio > 1000)
                            .OrderBy(p => p.Nombre)
                            .ToList();



### **ARQUITECTURA DE EF CORE**


**Cómo funciona EF Core internamente**

- **Modelo (Clases C#)** --> Representan tus tablas y campos.
- **DbContext** --> Clase puente entre tu aplicación y la base de datos.
- **Proveedores de bases de datos** --> Cada motor usa un provider especifico (SQL Server, MySQL, etc.).
- **Base de datos** --> EF Core traduce tus operaciones a SQL nativo.


Ejemplo:

public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
}










### **Code First**
Se refiere a crear primero el código en C# y a partir de este cerar la base de datos. 



**Flujo básico**
- definir modelos: crear clases C# que representen las tablas.
- Configurar DbContext: es el puente entre tu app y la base de datos.
- Crear migraciones: EF Core genera isntrucfciones SQL para crear la base de datos.
- Actualziar la base de datos: las migraciones se aplican y la base de datos se construye.


**Ejecutar migraciones**
dotnet ef migrations add Inicial --> agregar el nombre en referencia a lo que se esta realizando.
dotnet ef database update --> para que se apliquen los canmbios.

Todo esto se realza en consola.







### **Database First**
Se refiere a cerar el código a partir de una base de datos ya existente, aplicando un comando en la ocnsola se ceran las clases/ modelos.


dotnet ef dbcontext scaffold
"Server=.:Database=MiDb;Trusted_Connection=True;"
Microsoft.EntityFrameworkCore.SqlServer


Este comando escanea la base de datos y genera el modelo y el conetxto en tu proyecto.


**Flujo básico**


Cómo funciona Database First:
- Base de datos existente: ya tiene tablas y relaciones ceradas.
- Scaffolding: EF Core genera el codigo automáticamente.
- DbContext generado: contiene las configuraciones y conexiones.
- Uso directo: empiezas a hacer consultas y operaciones CRUD.