using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ProgramacionConfiguration : IEntityTypeConfiguration<Programacion>
{
    public void Configure(EntityTypeBuilder<Programacion> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("programacion");

        builder.HasIndex(e => e.IdContrato, "fk_contrato_programacion");

        builder.HasIndex(e => e.IdEmpleado, "fk_persona_programacion");

        builder.HasIndex(e => e.IdTurno, "fk_turno_programacion");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.IdContrato).HasColumnType("int(11)").HasColumnName("id_contrato");
        builder.Property(e => e.IdEmpleado).HasColumnType("int(11)").HasColumnName("id_empleado");
        builder.Property(e => e.IdTurno).HasColumnType("int(11)").HasColumnName("id_turno");

        builder
            .HasOne(d => d.IdContratoNavigation)
            .WithMany(p => p.Programacions)
            .HasForeignKey(d => d.IdContrato)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_contrato_programacion");

        builder
            .HasOne(d => d.IdEmpleadoNavigation)
            .WithMany(p => p.Programacions)
            .HasForeignKey(d => d.IdEmpleado)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_persona_programacion");

        builder
            .HasOne(d => d.IdTurnoNavigation)
            .WithMany(p => p.Programacions)
            .HasForeignKey(d => d.IdTurno)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_turno_programacion");
    }
}
