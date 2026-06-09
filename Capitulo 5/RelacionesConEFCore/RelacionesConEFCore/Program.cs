using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RelacionesConEFCore.Datos.ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //Instanciamos el contexto
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    ////========== INSERTAR DATOS ==========
    //// 1. Categoria
    //var categoria = new Categoria { Nombre = "Programacion" };

    //// 2. Etiquetas
    //var etiqueta1 = new Etiqueta { Nombre = "EFCore" };
    //var etiqueta2 = new Etiqueta { Nombre = ".NET 10" };

    //// 3. Usuario
    //var usuario = new Usuario
    //{
    //    Nombre = "Carlos",
    //    Email = "carlos@blog.com"
    //};

    //// 4. PerfilUsuario (1-1 con usuario)
    //usuario.Perfil = new PerfilUsuario
    //{
    //    Biografia = "Desarrollador full stack",
    //    FotoUrl = "https://mi-foto.com/carlos,.jpg"
    //};

    //// 5. Articulo (pertenece a Categoria y tiene etiquetas)
    //var articulo = new Articulo
    //{
    //    Titulo = "Novedades en EF Core 10",
    //    Contenido = "Explicamos las nuevas caracteristicas...",
    //    Categoria = categoria,
    //    Etiquetas = new List<Etiqueta> { etiqueta1, etiqueta2}
    //};

    //// 6. Comentarios (Dependen de Usuario y Articulo)
    //var comentario1 = new Comentario
    //{
    //    Texto = "Muy buen articulo",
    //    Usuario = usuario,
    //    Articulo = articulo
    //};
    //var comentario2 = new Comentario
    //{
    //    Texto = "Tengo una duda con las migraciones",
    //    Usuario = usuario,
    //    Articulo = articulo
    //};

    ////Agregar todo al contexto
    //context.Add(categoria);
    //context.AddRange(etiqueta1, etiqueta2);
    //context.Add(usuario);
    //context.Add(articulo);
    //context.AddRange(comentario1,comentario2);

    ////Guardar cambios
    //context.SaveChanges();

    //Console.WriteLine("Datos insertados correctamente");

    Console.WriteLine("== Lectura de datos con relaciones ==");
    var articulos = context.Articulos
        .Include(a => a.Categoria)//relacion 1:N (Articulo -> Categoria) Muestreme ariculo con su categoria.
        .Include(b => b.Etiquetas)//relacion N:N (articulo <-> Etiquetas)
        .Include(a => a.Comentarios)//relacion 1:N (Articulo -> Comentarios)
            .ThenInclude(c => c.Usuario)// relacion Comentario -> Usuario
            .ThenInclude(u => u.Perfil)//relacion Usuario -> PerfilUsuario
        .ToList();

    foreach (var articulo in articulos)
    {
        Console.WriteLine($"Articulo: {articulo.Titulo}");
        Console.WriteLine($"Categoria: {articulo.Categoria.Nombre}");
        Console.WriteLine($"Etiquetas: {string.Join(", ", articulo.Etiquetas.Select(e => e.Nombre))}");

        foreach (var comentario in articulo.Comentarios)
        {
            Console.WriteLine($"{comentario.Texto} (Por: {comentario.Usuario.Nombre})");
            Console.WriteLine($"Perfil: {comentario.Usuario.Perfil.Biografia})");
        }
    }
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
