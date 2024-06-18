using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutoVidaSv.Models;

public partial class AutovidasvContext : DbContext
{
    public AutovidasvContext()
    {
    }

    public AutovidasvContext(DbContextOptions<AutovidasvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Albumcar> Albumcars { get; set; }

    public virtual DbSet<Carro> Carros { get; set; }

    public virtual DbSet<Detallemovimiento> Detallemovimientos { get; set; }

    public virtual DbSet<Facturacion> Facturacions { get; set; }

    public virtual DbSet<Renta> Rentas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-79PTN5E;Database=AUTOVIDASV;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albumcar>(entity =>
        {
            entity.HasKey(e => e.Albumcarid).HasName("PK__ALBUMCAR__051572ED02949110");

            entity.ToTable("ALBUMCAR");

            entity.Property(e => e.Albumcarid).HasColumnName("ALBUMCARID");
            entity.Property(e => e.Imagen).HasColumnName("IMAGEN");
        });

        modelBuilder.Entity<Carro>(entity =>
        {
            entity.HasKey(e => e.Autoid).HasName("PK__CARROS__ADEE1E089EFA89BE");

            entity.ToTable("CARROS");

            entity.Property(e => e.Autoid).HasColumnName("AUTOID");
            entity.Property(e => e.Albumcarid).HasColumnName("ALBUMCARID");
            entity.Property(e => e.Anio).HasColumnName("ANIO");
            entity.Property(e => e.Combustible)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("COMBUSTIBLE");
            entity.Property(e => e.Marca)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("MARCA");
            entity.Property(e => e.Modelo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("MODELO");
            entity.Property(e => e.Transmicion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TRANSMICION");

            entity.HasOne(d => d.Albumcar).WithMany(p => p.Carros)
                .HasForeignKey(d => d.Albumcarid)
                .HasConstraintName("FK_CARROS_ALBUMCAR");
        });

        modelBuilder.Entity<Detallemovimiento>(entity =>
        {
            entity.HasKey(e => e.Detallemovimientoid).HasName("PK__DETALLEM__06A93D8E84EF6029");

            entity.ToTable("DETALLEMOVIMIENTOS");

            entity.Property(e => e.Detallemovimientoid).HasColumnName("DETALLEMOVIMIENTOID");
            entity.Property(e => e.Autoid).HasColumnName("AUTOID");
            entity.Property(e => e.Facturacionid).HasColumnName("FACTURACIONID");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.Rentaid).HasColumnName("RENTAID");
            entity.Property(e => e.Usuarioid).HasColumnName("USUARIOID");
            entity.Property(e => e.Ventaid).HasColumnName("VENTAID");

            entity.HasOne(d => d.Auto).WithMany(p => p.Detallemovimientos)
                .HasForeignKey(d => d.Autoid)
                .HasConstraintName("FK_DETALLEMOVIMIENTOS_AUTOS");

            entity.HasOne(d => d.Renta).WithMany(p => p.Detallemovimientos)
                .HasForeignKey(d => d.Rentaid)
                .HasConstraintName("FK_DETALLEMOVIMIENTOS_RENTA");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detallemovimientos)
                .HasForeignKey(d => d.Ventaid)
                .HasConstraintName("FK_DETALLEMOVIMIENTOS_VENTA");
        });

        modelBuilder.Entity<Facturacion>(entity =>
        {
            entity.HasKey(e => e.Facturacionid).HasName("PK__FACTURAC__A6BAA8D6D0C80B41");

            entity.ToTable("FACTURACION");

            entity.Property(e => e.Facturacionid).HasColumnName("FACTURACIONID");
            entity.Property(e => e.Detallemovimientoid).HasColumnName("DETALLEMOVIMIENTOID");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.Usuairoid).HasColumnName("USUAIROID");

            entity.HasOne(d => d.Detallemovimiento).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.Detallemovimientoid)
                .HasConstraintName("FK_FACTURACION_DETALLEMOVIMIENTOS");

            entity.HasOne(d => d.Usuairo).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.Usuairoid)
                .HasConstraintName("FK_FACTURACION_USUARIOS");
        });

        modelBuilder.Entity<Renta>(entity =>
        {
            entity.HasKey(e => e.Rentaid).HasName("PK__RENTAS__707E0633DF9E1E00");

            entity.ToTable("RENTAS");

            entity.Property(e => e.Rentaid).HasColumnName("RENTAID");
            entity.Property(e => e.Agregardiasextra).HasColumnName("AGREGARDIASEXTRA");
            entity.Property(e => e.Fechadevolucion).HasColumnName("FECHADEVOLUCION");
            entity.Property(e => e.Fechaentrega).HasColumnName("FECHAENTREGA");
            entity.Property(e => e.Totalacancelar)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("TOTALACANCELAR");
            entity.Property(e => e.Totaldedias).HasColumnName("TOTALDEDIAS");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Rolid).HasName("PK__ROLES__F92D7784A0ABAACF");

            entity.ToTable("ROLES");

            entity.Property(e => e.Rolid).HasColumnName("ROLID");
            entity.Property(e => e.Nivel).HasColumnName("NIVEL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuarioid).HasName("PK__USUARIOS__87968D3096D1AB41");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.Usuarioid).HasColumnName("USUARIOID");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTRASENA");
            entity.Property(e => e.Nombres)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.Rolid).HasColumnName("ROLID");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Ventaid).HasName("PK__VENTAS__74CBA8DABBB7711C");

            entity.ToTable("VENTAS");

            entity.Property(e => e.Ventaid).HasColumnName("VENTAID");
            entity.Property(e => e.Autoid).HasColumnName("AUTOID");
            entity.Property(e => e.Fechaventa).HasColumnName("FECHAVENTA");
            entity.Property(e => e.Valorventa)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("VALORVENTA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
