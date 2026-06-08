using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Aquí agregar los modelos
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
    }
}
