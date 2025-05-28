using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ControlDeGastosNetCore.Viewmodels;

namespace ControlDeGastosNetCore.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Presupuesto> Presupuestos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=Antares;Initial Catalog=ControlDeGastosDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Categorias");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Gastos");

            entity.HasIndex(e => e.CategoriaId, "IX_CategoriaId");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_dbo.Gastos_dbo.Categorias_CategoriaId");
        });


        modelBuilder.Entity<Presupuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Presupuestos");

            entity.Property(e => e.Año).HasColumnType("datetime");
            entity.Property(e => e.Mes).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
