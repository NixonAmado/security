using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("departamento");

        builder.HasIndex(e => e.IdPais, "fk_pais");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.IdPais).HasColumnType("int(11)").HasColumnName("id_pais");
        builder.Property(e => e.Nombre).HasMaxLength(255).HasColumnName("nombre");

        builder
            .HasOne(d => d.IdPaisNavigation)
            .WithMany(p => p.Departamentos)
            .HasForeignKey(d => d.IdPais)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_pais");
    }
}
