using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("contrato");

        builder.HasIndex(e => e.IdCliente, "fk_cliente_contrato");

        builder.HasIndex(e => e.IdEmpleado, "fk_empleado_contrato");

        builder.HasIndex(e => e.IdEstado, "fk_estado_contrato");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.FechaContrato).HasColumnName("fecha_contrato");
        builder.Property(e => e.FechaFin).HasColumnName("fecha_fin");
        builder.Property(e => e.IdCliente).HasColumnType("int(11)").HasColumnName("id_cliente");
        builder.Property(e => e.IdEmpleado).HasColumnType("int(11)").HasColumnName("id_empleado");
        builder.Property(e => e.IdEstado).HasColumnType("int(11)").HasColumnName("id_estado");

        builder
            .HasOne(d => d.IdClienteNavigation)
            .WithMany(p => p.ContratoIdClienteNavigations)
            .HasForeignKey(d => d.IdCliente)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_cliente_contrato");

        builder
            .HasOne(d => d.IdEmpleadoNavigation)
            .WithMany(p => p.ContratoIdEmpleadoNavigations)
            .HasForeignKey(d => d.IdEmpleado)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_empleado_contrato");

        builder
            .HasOne(d => d.IdEstadoNavigation)
            .WithMany(p => p.Contratos)
            .HasForeignKey(d => d.IdEstado)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_estado_contrato");
    }
}
