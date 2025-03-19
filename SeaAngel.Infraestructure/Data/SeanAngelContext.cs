using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Infraestructure.Data;

public partial class SeanAngelContext : DbContext
{
    public SeanAngelContext(DbContextOptions<SeanAngelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barco> Barco { get; set; }

    public virtual DbSet<BarcoHabitacion> BarcoHabitacion { get; set; }

    public virtual DbSet<Complementos> Complementos { get; set; }

    public virtual DbSet<Crucero> Crucero { get; set; }

    public virtual DbSet<Destino> Destino { get; set; }

    public virtual DbSet<DetPasajero> DetPasajero { get; set; }

    public virtual DbSet<DetReserva> DetReserva { get; set; }

    public virtual DbSet<EncReserva> EncReserva { get; set; }

    public virtual DbSet<Fecha> Fecha { get; set; }

    public virtual DbSet<FechaHabitacion> FechaHabitacion { get; set; }

    public virtual DbSet<Habitacion> Habitacion { get; set; }

    public virtual DbSet<Itinerario> Itinerario { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Puerto> Puerto { get; set; }

    public virtual DbSet<ReservaComplementos> ReservaComplementos { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Barco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Barco__3214EC27855A6158");

            entity.HasIndex(e => e.Nombre, "UQ__Barco__75E3EFCF5A15FA67").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<BarcoHabitacion>(entity =>
        {
            entity.HasKey(e => new { e.Idbarco, e.Idhabitacion }).HasName("PK__BarcoHab__1A616AAF18C539FC");

            entity.Property(e => e.Idbarco).HasColumnName("IDBarco");
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");

            entity.HasOne(d => d.IdbarcoNavigation).WithMany(p => p.BarcoHabitacion)
                .HasForeignKey(d => d.Idbarco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BarcoHabitacion_Barco");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.BarcoHabitacion)
                .HasForeignKey(d => d.Idhabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BarcoHabitacion_Habitacion");
        });

        modelBuilder.Entity<Complementos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compleme__3214EC27E85C599B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Aplicacion).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Crucero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Crucero__3214EC27A32FCDF4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idbarco).HasColumnName("IDBarco");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdbarcoNavigation).WithMany(p => p.Crucero)
                .HasForeignKey(d => d.Idbarco)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Crucero__IDBarco__47DBAE45");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Destino__3214EC2704523B2C");

            entity.HasIndex(e => e.Nombre, "UQ__Destino__75E3EFCFFDA0B8F5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<DetPasajero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Huespede__84BAE21EADE2CBF8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apelldio).HasMaxLength(50);
            entity.Property(e => e.DocumentoIdentidad).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.IdencReserva).HasColumnName("IDEncReserva");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.IdencReservaNavigation).WithMany(p => p.DetPasajero)
                .HasForeignKey(d => d.IdencReserva)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Huespedes__Reser__4F7CD00D");
        });

        modelBuilder.Entity<DetReserva>(entity =>
        {
            entity.HasKey(e => new { e.IdencReserva, e.Idhabitacion }).HasName("PK__Detalles__9B4CBCC0F4424C55");

            entity.Property(e => e.IdencReserva).HasColumnName("IDEncReserva");
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");

            entity.HasOne(d => d.IdencReservaNavigation).WithMany(p => p.DetReserva)
                .HasForeignKey(d => d.IdencReserva)
                .HasConstraintName("FK__DetallesR__Reser__4BAC3F29");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.DetReserva)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK__DetallesR__Habit__4CA06362");
        });

        modelBuilder.Entity<EncReserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservas__C3993703F0BC594E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CantidadDeCamarotes)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CantidadDePasajeros).HasMaxLength(50);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Idcrucero).HasColumnName("IDCrucero");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Impuesto)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.PrecioTotal)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.PrecioTotalCamorotes)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Subtotal)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdcruceroNavigation).WithMany(p => p.EncReserva)
                .HasForeignKey(d => d.Idcrucero)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Reservas__Crucer__45F365D3");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.EncReserva)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Reservas__Usuari__44FF419A");
        });

        modelBuilder.Entity<Fecha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FechasPr__3214EC27C78808D8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcrucero).HasColumnName("IDCrucero");

            entity.HasOne(d => d.IdcruceroNavigation).WithMany(p => p.Fecha)
                .HasForeignKey(d => d.Idcrucero)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__FechasPre__IDCru__4D94879B");
        });

        modelBuilder.Entity<FechaHabitacion>(entity =>
        {
            entity.HasKey(e => new { e.Idhabitacion, e.Idfecha });

            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idfecha).HasColumnName("IDFecha");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdfechaNavigation).WithMany(p => p.FechaHabitacion)
                .HasForeignKey(d => d.Idfecha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FechaHabitacion_Fecha");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.FechaHabitacion)
                .HasForeignKey(d => d.Idhabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FechaHabitacion_Habitacion");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__3214EC27459B7AAF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TamanoM2).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => new { e.Idcrucero, e.Idpuerto, e.Dia }).HasName("PK__Itinerar__3214EC27A1672406");

            entity.Property(e => e.Idcrucero).HasColumnName("IDCrucero");
            entity.Property(e => e.Idpuerto).HasColumnName("IDPuerto");
            entity.Property(e => e.Descripcion).HasMaxLength(255);

            entity.HasOne(d => d.IdcruceroNavigation).WithMany(p => p.Itinerario)
                .HasForeignKey(d => d.Idcrucero)
                .HasConstraintName("FK__Itinerari__IDCru__4F7CD00D");

            entity.HasOne(d => d.IdpuertoNavigation).WithMany(p => p.Itinerario)
                .HasForeignKey(d => d.Idpuerto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Itinerari__IDPue__5070F446");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__3214EC2742EF722F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .HasColumnName("CVV");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdencReserva).HasColumnName("IDEncReserva");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroTarjeta).HasMaxLength(16);
            entity.Property(e => e.TitularTarjeta).HasMaxLength(100);

            entity.HasOne(d => d.IdencReservaNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdencReserva)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pagos__ReservaID__5629CD9C");
        });

        modelBuilder.Entity<Puerto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Puerto__3214EC27C84A6EC4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iddestino).HasColumnName("IDDestino");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Pais).HasMaxLength(50);

            entity.HasOne(d => d.IddestinoNavigation).WithMany(p => p.Puerto)
                .HasForeignKey(d => d.Iddestino)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Puerto__IDDestin__52593CB8");
        });

        modelBuilder.Entity<ReservaComplementos>(entity =>
        {
            entity.HasKey(e => new { e.Idreserva, e.Idcomplemento });

            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Idcomplemento).HasColumnName("IDComplemento");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcomplementoNavigation).WithMany(p => p.ReservaComplementos)
                .HasForeignKey(d => d.Idcomplemento)
                .HasConstraintName("FK__ReservaCo__Compl__534D60F1");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.ReservaComplementos)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK__ReservaCo__Reser__52593CB8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC279523C7B6");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuario__531402F39FBAAF29").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Pais).HasMaxLength(50);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .HasDefaultValue("Cliente");
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
