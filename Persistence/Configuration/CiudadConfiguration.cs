using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("ciudad");

        builder.HasIndex(e => e.IdDepartamento, "fk_departamento");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder
            .Property(e => e.IdDepartamento)
            .HasColumnType("int(11)")
            .HasColumnName("id_departamento");
        builder.Property(e => e.Nombre).HasMaxLength(255).HasColumnName("nombre");

        builder
            .HasOne(d => d.IdDepartamentoNavigation)
            .WithMany(p => p.Ciudades)
            .HasForeignKey(d => d.IdDepartamento)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_departamento");
    }
}
