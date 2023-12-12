using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CategoriaPerConfiguration : IEntityTypeConfiguration<CategoriaPer>
{
    public void Configure(EntityTypeBuilder<CategoriaPer> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("categoriaPer");

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnType("int(11)")
            .HasColumnName("id");
        builder.Property(e => e.Descripcion).HasMaxLength(255).HasColumnName("descripcion");
    }
}
