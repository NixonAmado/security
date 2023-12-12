using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ContactoPerConfiguration : IEntityTypeConfiguration<ContactoPer>
{
    public void Configure(EntityTypeBuilder<ContactoPer> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("contactoPer");

        builder.HasIndex(e => e.IdPersona, "fk_persona_contacto");

        builder.HasIndex(e => e.IdTipoContacto, "fk_tipo_contacto_contacto");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.Descripcion).HasMaxLength(255).HasColumnName("descripcion");
        builder.Property(e => e.IdPersona).HasColumnType("int(11)").HasColumnName("id_persona");
        builder
            .Property(e => e.IdTipoContacto)
            .HasColumnType("int(11)")
            .HasColumnName("id_tipo_contacto");

        builder
            .HasOne(d => d.IdPersonaNavigation)
            .WithMany(p => p.ContactoPers)
            .HasForeignKey(d => d.IdPersona)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_persona_contacto");

        builder
            .HasOne(d => d.IdTipoContactoNavigation)
            .WithMany(p => p.ContactoPers)
            .HasForeignKey(d => d.IdTipoContacto)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_tipo_contacto_contacto");
    }
}
