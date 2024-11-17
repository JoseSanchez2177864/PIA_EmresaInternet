using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

public partial class PiaInternetContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public PiaInternetContext()
    {
    }

    public PiaInternetContext(DbContextOptions<PiaInternetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<Opinione> Opiniones { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => new { e.IdVenta, e.IdPaquete }).HasName("PK__DetalleV__E13BEC213BB480B7");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__idPaq__4222D4EF");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__idVen__412EB0B6");
        });

        modelBuilder.Entity<Opinione>(entity =>
        {
            entity.HasKey(e => e.IdOpinion).HasName("PK__Opinione__83A5899BDF0E477B");

            entity.Property(e => e.IdOpinion).ValueGeneratedNever();

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Opiniones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Opiniones__idUsu__44FF419A");
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquetes__646BA35FFA826D83");

            entity.Property(e => e.IdPaquete).ValueGeneratedNever();
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__077D5614147537BC");

            entity.Property(e => e.IdVenta).ValueGeneratedNever();

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__idUsuari__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
