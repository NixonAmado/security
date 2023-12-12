using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("persona");

        builder.HasIndex(e => e.IdCategoria, "fk_categoria_persona");

        builder.HasIndex(e => e.IdCiudad, "fk_ciudad_persona");

        builder.HasIndex(e => e.IdTipoPersona, "fk_tipo_persona_persona");

        builder.HasIndex(e => e.IdPersona, "id_persona").IsUnique();

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder
            .Property(e => e.FechaRegistro)
            .HasColumnType("datetime")
            .HasColumnName("fechaRegistro");
        builder.Property(e => e.IdCategoria).HasColumnType("int(11)").HasColumnName("id_categoria");
        builder.Property(e => e.IdCiudad).HasColumnType("int(11)").HasColumnName("id_ciudad");
        builder.Property(e => e.IdPersona).HasColumnType("int(11)").HasColumnName("id_persona");
        builder
            .Property(e => e.IdTipoPersona)
            .HasColumnType("int(11)")
            .HasColumnName("id_tipo_persona");
        builder.Property(e => e.Nombre).HasMaxLength(255).HasColumnName("nombre");

        builder
            .HasOne(d => d.IdCategoriaNavigation)
            .WithMany(p => p.Personas)
            .HasForeignKey(d => d.IdCategoria)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_categoria_persona");

        builder
            .HasOne(d => d.IdCiudadNavigation)
            .WithMany(p => p.Personas)
            .HasForeignKey(d => d.IdCiudad)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_ciudad_persona");

        builder
            .HasOne(d => d.IdTipoPersonaNavigation)
            .WithMany(p => p.Personas)
            .HasForeignKey(d => d.IdTipoPersona)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_tipo_persona_persona");
    }
}
