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
"Server=.:Database=MiDb;TrustServerCertificate=True;"
Microsoft.EntityFrameworkCore.SqlServer


Este comando escanea la base de datos y genera el modelo y el conetxto en tu proyecto.


**Flujo básico**


Cómo funciona Database First:
- Base de datos existente: ya tiene tablas y relaciones ceradas.
- Scaffolding: EF Core genera el codigo automáticamente.
- DbContext generado: contiene las configuraciones y conexiones.
- Uso directo: empiezas a hacer consultas y operaciones CRUD.



#### **Crear un proyecto MVC (Modelo Vista Controlador)**

En VS Code: dotnet new mvc -n NombreDelProyecto
Instalar paquetes: 
    - dotnet add package Microsoft.EntityFrameworkCore -->Núcleo EF Core
    - dotnet add package Microsoft.EntityFrameworkCore.SqlServer --> Proveedor de base de datos
    - dotnet add package Microsoft.EntityFrameworkCore.Design
    - dotnet tool install --global dotnet-ef  --> herramientas de CLI para usar comandos ef
    - dotnet list package --> listar los paquetes instaaldos en .csproj


Docker
CampoValorServer namelocalhost,1433AuthenticationSQL Server AuthenticationLoginsaPasswordSqlDev2024!


### **DBCONTEXT**
- Es el puente entre la app y la base de datos
- Es la cl ase principal de EF Core
- Permite consultar, insertar, actualizar y eliminar datos usando objetos C#
Contiene DbSet<T>, que representa las tablas de las bases de datos.

Ejemplo

public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=.;Database=MiBD;TrustServerCertificate=True;");
    }
}


#### **Función del DbContext**

 Flujo básico:

 1. Configurar el DbContext --> define conexión y tablas.
 2. Usas DbSet<T> --> Accede a las entidades.
 3. Guardas cambios --> EF Core traduce operaciones a SQL.


 Ejemplo CRUD:

 using (var context = new AppDbContext())
 {
    //Insertar
    context.Productos.Add(new Producto { Nombre = "Teclado", Precio = 150});
    context.SaveChanges():

    //Consultar
    var lista = context.Productos.ToList();
    foreach (var p in lista)
    Console.WriteLine($"{p.Nombre} - ${p.Precio}");
 }

 
* Un solo DbContext por unidad de trabajo


### **Crear el servicio del DbContext**
1. En program.cs agregar el builder: builder.Services.AddDbContext<CEFCore.Data.ApplicationDbContext>();
2. Como en la ruta que se agregó en el builder hay una carpeta llamada Data y una clase llamada Application DbContext, los agregaremos al proyecto.
3. Al crear la clase ApplicationDbContext (la cual puede llamarse como sea) debemos heredarla de la clase DbContext. Pedirá importar la librería Microsoft EF Core (using Microsoft.EntityFrameworkCore;). No olvidar agregar el constructor con options. public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}
4. Ahora en el builder podemos importar con using la ruta hacia la carperta datos y solo agregar el nombre d ela clase: builder.Services.AddDbContext<ApplicationDbContext>();
5. Ahora hay que agregarle al builder la cadena de conexión: 
    builder.Services.AddDbContext<CEFCore.Data.ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));


Apartir de aquí ya se puede hacer una migración ya que ya existe una conexión en el dbContext. Como no hay aun modelos ni clases que refieran a tablas. Sólos e creará la base de datos que se indicó en el Connectionstring.


En consola: dotnet ef migrations add InitialCreate --- dotnet ef database update