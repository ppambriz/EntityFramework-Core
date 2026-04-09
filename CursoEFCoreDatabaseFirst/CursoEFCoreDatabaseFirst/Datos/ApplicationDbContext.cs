using System;
using System.Collections.Generic;
using CursoEFCoreDatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCoreDatabaseFirst.Datos;

public partial class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<categoria> categorias { get; set; }
    public virtual DbSet<nota> notas { get; set; }
    public virtual DbSet<usuario> usuarios { get; set; }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
