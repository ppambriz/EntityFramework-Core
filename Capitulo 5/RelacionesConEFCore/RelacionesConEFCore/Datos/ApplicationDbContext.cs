using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }//En el context se agrega el modelo en plural.
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilUsuario> PerfilesUsuarios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
