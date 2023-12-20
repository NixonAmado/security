using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            {
                builder.ToTable("user");

                builder.Property(p => p.Id).IsRequired();

                builder
                    .Property(p => p.Username)
                    .HasColumnName("userName")
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired();

                builder
                    .Property(p => p.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar")
                    .HasMaxLength(255)
                    .IsRequired();

                builder
                    .Property(p => p.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar")
                    .HasMaxLength(100)
                    .IsRequired();

                builder
                    .HasMany(u => u.Rols)
                    .WithMany(r => r.Users)
                    .UsingEntity<UserRol>(
                        j =>
                            j.HasOne(pt => pt.Rol)
                                .WithMany(t => t.UsersRols)
                                .HasForeignKey(ut => ut.RolId),
                        j =>
                            j.HasOne(et => et.User)
                                .WithMany(et => et.UsersRols)
                                .HasForeignKey(el => el.UserId),
                        j =>
                        {
                            j.ToTable("userRol");
                            j.HasKey(t => new { t.UserId, t.RolId });
                        }
                    );

                builder
                    .HasMany(p => p.RefreshTokens)
                    .WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId);
            }
        }
    }
}
