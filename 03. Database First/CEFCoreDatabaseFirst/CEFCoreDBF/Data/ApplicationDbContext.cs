using System;
using System.Collections.Generic;
using CEFCoreDBF.Models;
using Microsoft.EntityFrameworkCore;

namespace CEFCoreDBF.Data;

public partial class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<categoria> categoria { get; set; }

    public virtual DbSet<nota> notas { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<categoria>(entity =>
        {
            entity.Property(e => e.activo).HasDefaultValue(true, "DF_categoria_activo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}