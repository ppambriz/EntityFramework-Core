using CursoEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityFrameworkCore.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
