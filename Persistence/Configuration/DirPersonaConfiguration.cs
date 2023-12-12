using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class DirPersonaConfiguration : IEntityTypeConfiguration<DirPersona>
{
    public void Configure(EntityTypeBuilder<DirPersona> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("dirPersona");

        builder.HasIndex(e => e.IdPersona, "fk_persona_direccion");

        builder.HasIndex(e => e.IdTipoDireccion, "fk_tipo_direccion_direccion");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.Descripcion).HasMaxLength(255).HasColumnName("descripcion");
        builder.Property(e => e.IdPersona).HasColumnType("int(11)").HasColumnName("id_persona");
        builder
            .Property(e => e.IdTipoDireccion)
            .HasColumnType("int(11)")
            .HasColumnName("id_tipo_direccion");

        builder
            .HasOne(d => d.IdPersonaNavigation)
            .WithMany(p => p.DirPersonas)
            .HasForeignKey(d => d.IdPersona)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_persona_direccion");

        builder
            .HasOne(d => d.IdTipoDireccionNavigation)
            .WithMany(p => p.DirPersonas)
            .HasForeignKey(d => d.IdTipoDireccion)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_tipo_direccion_direccion");
    }
}
