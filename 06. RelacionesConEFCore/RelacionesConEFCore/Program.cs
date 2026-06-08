using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //Instanciamos el contexto
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    //// ======== INSERTAR DATOS ========
    //// 1. Categoría
    //var categoria = new Categoria { Nombre = "Programación" };

    //// 2. Etiquetas
    //var etiqueta1 = new Etiqueta { Nombre = "EFCore" };
    //var etiqueta2 = new Etiqueta { Nombre = ".NET 10" };

    //// 3. Usuario
    //var usuario = new Usuario
    //{
    //    Nombre = "Carlos",
    //    Email = "carlog@blog.co"
    //};

    //// 4. PerfilUsuario (1–1 con Usuario)
    //usuario.Perfil = new PerfilUsuario
    //{
    //    Biografia = "Desarrollador full stack",
    //    FotoUrl = "https://mi-foto.com/carlos.jpg"
    //};

    //// 5. Artículo (pertenece a Categoría y tiene etiquetas)
    //var articulo = new Articulo
    //{
    //    Titulo = "Novedades en EF Core 10",
    //    Contenido = "Explicamos las nuevas características...",
    //    Categoria = categoria,
    //    Etiquetas = new List<Etiqueta> { etiqueta1, etiqueta2 }
    //};

    //// 6. Comentarios (dependen de Usuario y Articulo)
    //var comentario1 = new Comentario
    //{
    //    Texto = "Muy buen artículo",
    //    Usuario = usuario,
    //    Articulo = articulo
    //};

    //var comentario2 = new Comentario
    //{
    //    Texto = "Tengo una duda con las migraciones",
    //    Usuario = usuario,
    //    Articulo = articulo
    //};

    //// Agregar todo al contexto
    //context.Add(categoria);
    //context.AddRange(etiqueta1, etiqueta2);
    //context.Add(usuario);
    //context.Add(articulo);
    //context.AddRange(comentario1, comentario2);

    //// Guardar cambios
    //context.SaveChanges();

    //Console.WriteLine("Datos insertados correctamente.");

    Console.WriteLine("== Lectura de datos con relaciones ==");
    //carga diferida(lazy loading) o carga explícita(explicit loading) o carga ansiosa(eager loading).
    //var articulos = context.Articulos
    //    .Include(a => a.Categoria) // relación 1:N (Articulo ? Categoria)
    //    .Include(a => a.Etiquetas) // relación N:N (Articulo ? Etiquetas)
    //   .Include(a => a.Comentarios) // relación 1:N (Articulo ? Comentarios)
    //   .AsSplitQuery()// divide la consulta
    //                  // .ThenInclude(c => c.Usuario) // relación Comentario ? Usuario
    //                  //.ThenInclude(u => u.Perfil) // relación Usuario ? PerfilUsuario
    //    .ToList();


    //foreach (var articulo in articulos)
    //{
    //    Console.WriteLine($" Artículo: {articulo.Titulo}");
    //    Console.WriteLine($" Categoría: {articulo.Categoria.Nombre}");
    //    Console.WriteLine($" Etiquetas: {string.Join(", ", articulo.Etiquetas.Select(e => e.Nombre))}");

    //    //foreach (var comentario in articulo.Comentarios)
    //    //{
    //    //    Console.WriteLine($" {comentario.Texto} (por: {comentario.Usuario.Nombre})");
    //    //    Console.WriteLine($" Perfil: {comentario.Usuario.Perfil?.Biografia})");
    //    //}
    //}
    //Filtered Include (filtro en relaciones)
    //var articulos = context.Articulos       
    //   .Include(a => a.Comentarios 
    //    .Where(c => c.Aprobado))// filtro dentro del include
    //    .ToList();

    //foreach (var articulo in articulos)
    //{
    //    Console.WriteLine($" Artículo: {articulo.Titulo} (ID: {articulo.Id})");

    //    if (articulo.Comentarios != null && articulo.Comentarios.Any())
    //    {
    //        foreach (var comentario in articulo.Comentarios)
    //        {
    //            Console.WriteLine($"Comentario #{comentario.Id}: {comentario.Texto} - Aprobadeo: {comentario.Aprobado}");
    //        }
    //    }else
    //    {
    //        Console.WriteLine("(Sin comentarios aprobados)");
    //    }           

    //}

    //Consultas proyectadas (Select en relaciones)
    //Lista de artículos con título, nombre de categoría y cantidad de comentarios
    //var lista = context.Articulos
    //    .Select(a => new
    //    {
    //        a.Titulo,
    //        Categoria = a.Categoria.Nombre,
    //        CantidadComentarios = a.Comentarios.Count()
    //    })
    //    .ToList();

    //foreach(var item in lista)
    //    Console.WriteLine($"{item.Titulo} | {item.Categoria} | Comentarios: {item.CantidadComentarios}");

    //Carga explícita (Explicit Loading)
    //var articulo = context.Articulos.First();
    // Cargar comentarios de forma explícita
    //context.Entry(articulo)
    //    .Collection(a => a.Comentarios)
    //    .Load();


    // Buscar el primer artículo con su categoría
    //var articulo = context.Articulos
    //    .Include(a => a.Categoria)
    //    .First();

    //Console.WriteLine($" Antes: {articulo.Titulo}, Categoria:  {articulo.Categoria.Nombre}");

    //// Cambiar título y categoría
    //articulo.Titulo = "EF Core 10 - Tutorial Avanzado";
    //articulo.Categoria.Nombre = "Bases de Datos";

    //context.SaveChanges();
    //Console.WriteLine("Artículo y categoría actualizados.");

    // Buscar el usuario con su perfil con su categoría
    //var usuario = context.Usuarios
    //    .Include(a => a.Perfil)
    //    .First();

    //Console.WriteLine($" Antes: {usuario.Nombre}, Biografía:  {usuario.Perfil.Biografia}");

    //// Cambiar los datos
    //usuario.Nombre = "Carlos Actualizado";
    //usuario.Perfil.Biografia = "Arquitecto de software con experiencia en .NET";

    //context.SaveChanges();
    //Console.WriteLine($"Usuario {usuario.Nombre}, Biografía: {usuario.Perfil.Biografia}");
    //Console.WriteLine("Usuario y perfil actualizados.");

    //Actualizar Comentarios de un Artículo (relación 1–N)
    //var articuloConComentarios = context.Articulos
    //.Include(a => a.Comentarios)
    //.First();

    //Console.WriteLine($"Artículo: {articuloConComentarios.Titulo}");

    //// Cambiar el texto del primer comentario
    //var primerComentario = articuloConComentarios.Comentarios.First();
    //primerComentario.Texto = "Comentario editado por el autor.";

    //context.SaveChanges();

    //Console.WriteLine("Comentario actualizado.");

    //Actualizar Etiquetas en un Artículo (relación N–N)
    //var articuloConEtiquetas = context.Articulos
    //.Include(a => a.Etiquetas)
    //.First();

    //Console.WriteLine($"Antes: {string.Join(", ", articuloConEtiquetas.Etiquetas.Select(e => e.Nombre))}");

    //// Quitar una etiqueta y agregar otra
    //var etiquetaQuitar = articuloConEtiquetas.Etiquetas.First();
    //articuloConEtiquetas.Etiquetas.Remove(etiquetaQuitar);

    //var nuevaEtiqueta = new Etiqueta { Nombre = "DataAnnotations" };
    //articuloConEtiquetas.Etiquetas.Add(nuevaEtiqueta);

    //context.SaveChanges();

    //Console.WriteLine("Etiquetas actualizadas.");

    //Eliminar un Artículo y sus Comentarios(1–N)

    /*
     * EF Core genera:
        DELETE FROM Comentarios WHERE ArticuloId = X
        DELETE FROM Articulos WHERE Id = X
     */
    //var articulo = context.Articulos
    //.Include(a => a.Comentarios)
    //.First();

    //Console.WriteLine($"Eliminando artículo: {articulo.Titulo} con {articulo.Comentarios.Count} comentarios...");

    //context.Articulos.Remove(articulo);
    //context.SaveChanges();

    //Console.WriteLine("Artículo y sus comentarios eliminados (cascade delete).");

    //Eliminar un Usuario y su Perfil (1–1)

    //var usuario = context.Usuarios
    //.Include(u => u.Perfil)
    //.First();

    //Console.WriteLine($"Eliminando usuario: {usuario.Nombre} y su perfil asociado...");

    //context.Usuarios.Remove(usuario);
    //context.SaveChanges();

    //Console.WriteLine("Usuario y perfil eliminados (cascade delete).");

    //Eliminar una Etiqueta en relación N–N
    //var etiqueta = context.Etiquetas
    //.Include(e => e.Articulos)
    //.First();

    //Console.WriteLine($"Eliminando etiqueta: {etiqueta.Nombre}");

    //context.Etiquetas.Remove(etiqueta);
    //context.SaveChanges();

    //Console.WriteLine("Etiqueta eliminada y relaciones en tabla intermedia borradas.");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
