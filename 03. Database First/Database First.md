### **Database First**





##### **Modelos**



* Engoque de desarrollo donde partimos de una base de datos ya existente.
* EF Core genera automáticamente las clases de entidades y el DbContext.
* Ideal cuando el diseño de la base ya está definido o lo administra otro equipo.







##### **Flujo de trabajo**



* Diseñar o usar una base de datos existente.
* Ejecutar un comando scaffolding para generar modelos y DbContext.
* Usar esas clases en código para consultar, insertar y actualizar datos.
* Si cambia la base volver a ejecutar scaffolding para actualizar el modelo.



**Ejecución habitual**: 

dotnet ef dbcontext scaffold "<cadenaConexion>" Microsoft.EntityFrameworkCore.SqlServer -o Models  --> Se guardan en la carpeta Models



dotnet ef dbcontext scaffold "Server=localhost,1433;Database=CEFCoreNET10;User Id=sa;Password=SqlDev2024!;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -o Models



dotnet ef dbcontext scaffold   		 -------   Inicia el proceso de scaffolding

"Server=...;Database=...;..."   	 -------   La cadena de conexión a tu base de datos

Microsoft.EntityFrameworkCore.SqlServer  -------   El proveedor de base de datos a usar

\-o Models  				 -------   Carpeta de salida donde se generarán las clases (Output)









##### **Ejecución sugerida con buenas practicas:**

**General**

dotnet ef dbcontext scaffold "Name=ConnectionStrings:ConexionSQL" Microsoft.EntityFrameworkCore.SqlServer -o Models (--outputDir Models) --context-dir Data --context ApplicationDbContext --use-database-names --no-onconfiguring -f



**Ejemplo desde el ConnectionString:**
dotnet ef dbcontext scaffold "Name=ConnectionStrings:ConexionSQL" Microsoft.EntityFrameworkCore.SqlServer -o Models (--outputDir Models) --context-dir Data --context ApplicationDbContext --use-database-names --DataAnnotations --no-onconfiguring -d(o --data-annotations) -f



**Ejemplo con conexión directa**

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=notas\_php;User Id=sa;Password=SqlDev2024!" Microsoft.EntityFrameworkCore.SqlServer -o Models (--outputDir Models) --context-dir Data --context ApplicationDbContext --use-database-names --no-onconfiguring -d(o --data-annotations) -f





**Usada en el proyecto**

dotnet ef dbcontext scaffold "Name=ConnectionStrings:ConexionSQL" Microsoft.EntityFrameworkCore.SqlServer -o Models --use-database-names -d --context-dir Data --context ApplicationDbContext



**Rescaffold cuando se agrega una tabla** 



"Name=ConnectionStrings:ConexionSQL"   	-------   Lee la cadena desde appsettings.json en lugar de hardcodearla (más seguro)

\--context-dir Data   			-------   Pone el DbContext en la carpeta Data

\--context ApplicationDbContext  	-------   Nombre personalizado para el DbContext

\--use-database-names  			-------   Usa los nombres exactos de las tablas/columnas como están en SQL Server

\--no-onconfiguring			-------	  No genera el método OnConfiguring con la cadena hardcodeada (la registras en Program.cs)

\-d (o --data-annotations)		-------	  Se le indica que utilizará DataAnnotations

\-f					-------   Force, sobrescribe los archivos si ya existen





**Tablas específicas**

**Si solo quieres migrar algunas tablas y no toda la base de datos:**

dotnet ef dbcontext scaffold "Name=ConnectionStrings:ConexionSQL" Microsoft.EntityFrameworkCore.SqlServer -o Models --table Categorias --table Productos



**Importante para que funcione el Name=ConnectionStrings:...**

Necesitas tener instalado este paquete adicional en tu proyecto:

dotnet add package Microsoft.Extensions.Configuration.Json



**Y registrar el DbContext en Program.cs como ya tenías:**

builder.Services.AddDbContext<ApplicationDbContext>(options =>

&#x20;   options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));





###### **Comando para copiar respaldo de base de datos backup.bak a directorio de Docker**

docker cp "C:\\ruta\\a\\tu\\backup.bak" sqlserver-dev:/var/opt/mssql/backup.bak







