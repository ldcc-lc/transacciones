using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Domain.Entities;

namespace Transacciones.Infrastructure.Data
{
    public class TransaccionesDbContext : DbContext
    {
        public TransaccionesDbContext(DbContextOptions<TransaccionesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<TipoCambio> TiposCambio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TABLA MONEDA
            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.ToTable("MONEDA");

                entity.HasKey(e => e.Id)
                    .HasName("PK_MONEDA");

                entity.Property(e => e.Id)
                    .HasColumnName("PK_ID_MONEDA_IN");

                entity.Property(e => e.Codigo)
                    .HasColumnName("CODIGO_CH")
                    .HasColumnType("char(3)")
                    .IsRequired();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE_VC")
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION_DT")
                    .HasColumnType("datetime");

                entity.HasIndex(e => e.Codigo)
                    .IsUnique()
                    .HasDatabaseName("UQ_MONEDA_CODIGO");
            });

            // TABLA CUENTA
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.ToTable("CUENTA", table =>
                {
                    table.HasCheckConstraint("CK_CUENTA_SALDO", "SALDO_DE >= 0");
                });

                entity.HasKey(e => e.Id)
                    .HasName("PK_CUENTA");

                entity.Property(e => e.Id)
                    .HasColumnName("PK_ID_CUENTA_IN");

                entity.Property(e => e.MonedaId)
                    .HasColumnName("FK_ID_MONEDA_IN");

                entity.Property(e => e.NumeroCuenta)
                    .HasColumnName("NRO_CUENTA_VC")
                    .HasMaxLength(14)
                    .IsRequired();

                entity.Property(e => e.Tipo)
                    .HasColumnName("TIPO_CH")
                    .HasColumnType("char(3)")
                    .HasConversion<string>()
                    .IsRequired();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE_VC")
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(e => e.Saldo)
                    .HasColumnName("SALDO_DE")
                    .HasColumnType("decimal(12,2)")
                    .HasDefaultValue(0);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION_DT")
                    .HasColumnType("datetime");

                entity.HasIndex(e => e.NumeroCuenta)
                    .IsUnique()
                    .HasDatabaseName("UQ_CUENTA_NRO_CUENTA");

                entity.HasOne(e => e.Moneda)
                    .WithMany(e => e.Cuentas)
                    .HasForeignKey(e => e.MonedaId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_CUENTA_MONEDA");
            });

            // TABLA MOVIMIENTO
            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.ToTable("MOVIMIENTO", table =>
                {
                    table.HasCheckConstraint("CK_MOVIMIENTO_IMPORTE", "IMPORTE_DE > 0");
                });

                entity.HasKey(e => e.Id)
                    .HasName("PK_MOVIMIENTO");

                entity.Property(e => e.Id)
                    .HasColumnName("PK_ID_MOVIMIENTO_IN");

                entity.Property(e => e.CuentaId)
                    .HasColumnName("FK_ID_CUENTA_IN");

                entity.Property(e => e.Fecha)
                    .HasColumnName("FECHA_DT")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.Tipo)
                    .HasColumnName("TIPO_CH")
                    .HasColumnType("char(1)")
                    .HasConversion<string>()
                    .IsRequired();

                entity.Property(e => e.Importe)
                    .HasColumnName("IMPORTE_DE")
                    .HasColumnType("decimal(12,2)")
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION_DT")
                    .HasColumnType("datetime");

                entity.HasOne(e => e.Cuenta)
                    .WithMany(e => e.Movimientos)
                    .HasForeignKey(e => e.CuentaId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_MOVIMIENTO_CUENTA");
            });

            // TABLA TIPO_CAMBIO
            modelBuilder.Entity<TipoCambio>(entity =>
            {
                entity.ToTable("TIPO_CAMBIO", table =>
                {
                    table.HasCheckConstraint("CK_TIPO_CAMBIO_TASA", "TASA_DE > 0");
                });

                entity.HasKey(e => e.Id)
                    .HasName("PK_TIPO_CAMBIO");

                entity.Property(e => e.Id)
                    .HasColumnName("PK_ID_TIPO_CAMBIO_IN");

                entity.Property(e => e.MonedaOrigenId)
                    .HasColumnName("FK_ID_MONEDA_ORIGEN_IN");

                entity.Property(e => e.MonedaDestinoId)
                    .HasColumnName("FK_ID_MONEDA_DESTINO_IN");

                entity.Property(e => e.Fecha)
                    .HasColumnName("FECHA_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tasa)
                    .HasColumnName("TASA_DE")
                    .HasColumnType("decimal(12,6)");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("FECHA_MODIFICACION_DT")
                    .HasColumnType("datetime");

                entity.HasIndex(e => new
                {
                    e.MonedaOrigenId,
                    e.MonedaDestinoId,
                    e.Fecha
                })
                    .IsUnique()
                    .HasDatabaseName("UQ_TIPO_CAMBIO_MONEDAS_FECHA");

                entity.HasOne(e => e.MonedaOrigen)
                    .WithMany(e => e.TiposCambioOrigen)
                    .HasForeignKey(e => e.MonedaOrigenId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_TIPO_CAMBIO_MONEDA_ORIGEN");

                entity.HasOne(e => e.MonedaDestino)
                    .WithMany(e => e.TiposCambioDestino)
                    .HasForeignKey(e => e.MonedaDestinoId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_TIPO_CAMBIO_MONEDA_DESTINO");
            });
        }
    }
}