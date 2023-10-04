using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFolio.WebApi.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.FirstName)
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .HasMaxLength(50);

        builder.Property(p => p.PasswordHash)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastPasswordHash)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastPasswordHash)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(p => p.Role);
    }
}

