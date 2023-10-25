using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFolio.WebApi.Entities.Configurations;

public class DishTypeConfiguration : IEntityTypeConfiguration<DishType>
{
    public void Configure(EntityTypeBuilder<DishType> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(false);

    }
}

