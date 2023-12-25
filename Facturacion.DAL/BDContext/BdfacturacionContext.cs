using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Facturacion.Model;

namespace Facturacion.DAL.BDContext;

public partial class BdfacturacionContext : DbContext
{
    public BdfacturacionContext()
    {
    }

    public BdfacturacionContext(DbContextOptions<BdfacturacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> MenuRols { get; set; }

    public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; User=sa; Password=Sql@root123; DataBase=DBVENTA; TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C6A08EBA0");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CedulaCliente).HasName("PK__Cliente__D14E16622F5B8F3E");

            entity.ToTable("Cliente");

            entity.Property(e => e.CedulaCliente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedulaCliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F5009329B");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__idPro__5441852A");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__534D60F1");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF4837B005897");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__MenuRol__9D6D61A4187F6A1D");

            entity.ToTable("MenuRol");

            entity.Property(e => e.IdMenuRol).HasColumnName("idMenuRol");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRol__idMenu__3C69FB99");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MenuRol__idRol__3D5E1FD2");
        });

        modelBuilder.Entity<NumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__NumeroDo__471E421AAA8A9A07");

            entity.ToTable("NumeroDocumento");

            entity.Property(e => e.IdNumeroDocumento).HasColumnName("idNumeroDocumento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_Numero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A1327FCE5C11");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__idCate__48CFD27E");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76607A13BA");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6B76B8615");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__idRol__403A8C7D");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D5614A84EDF58");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.CedulaCliente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedulaCliente");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoPago");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.CedulaClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.CedulaCliente)
                .HasConstraintName("FK__Venta__cedulaCli__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
