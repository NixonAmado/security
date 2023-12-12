using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class TurnoConfiguration : IEntityTypeConfiguration<Turno>
{
    public void Configure(EntityTypeBuilder<Turno> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("turno");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.HoraTurnoFin).HasColumnType("time").HasColumnName("hora_turno_fin");
        builder
            .Property(e => e.HoraTurnoInicio)
            .HasColumnType("time")
            .HasColumnName("hora_turno_inicio");
        builder.Property(e => e.NombreTurno).HasMaxLength(255).HasColumnName("nombre_turno");
    }
}
