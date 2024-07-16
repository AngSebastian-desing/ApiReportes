using System;
using System.Collections.Generic;
using ApiReportes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiReportes.Data;

public partial class ReportesContext : DbContext
{
    public ReportesContext()
    {
    }

    public ReportesContext(DbContextOptions<ReportesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Folio).HasName("PK__REPORTE__E8F12C9E8815F0B4");

            entity.ToTable("REPORTE");

            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estatus");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .IsRequired()
                .HasColumnName("fecha");
            entity.Property(e => e.Titulo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.Imagen)
                .IsRequired()
                .HasColumnName("imagen")
                .HasColumnType("varchar");
            entity.Property(e => e.Fecha_autorizacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_autorizacion");
            entity.Property(e => e.Usuario_gestiona)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("usuario_gestiona");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_IDUSUARIO");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF9726FE3D33");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("puesto");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email")
                .IsRequired();
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password")
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
