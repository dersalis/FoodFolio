using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFolio.WebApi.Entities;

public class FoodFolioDbContext : DbContext
{
    private readonly string _connectionString = "Data Source=127.0.0.1; Initial Catalog=FoodFolio; User Id=sa; Password=ulkXZe9PKPEpli22SKR0; TrustServerCertificate=True";

    public DbSet<DishType> DishTypes { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}

//public class UserConfiguration : IEntityTypeConfiguration<User>
//{
//    public void Configure(EntityTypeBuilder<User> builder)
//    {
//        builder.Property(p => p.Id)
//            .IsRequired();

//        builder.Property(p => p.Email)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.FirstName)
//            .HasMaxLength(50);

//        builder.Property(p => p.LastName)
//            .HasMaxLength(50);

//        builder.Property(p => p.PasswordHash)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.LastPasswordHash)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.LastPasswordHash)
//            .IsRequired()
//            .HasDefaultValue(false);

//        builder.Property(p => p.IsActive)
//            .IsRequired()
//            .HasDefaultValue(false);

//        builder.HasOne(p => p.Role);
//    }
//}

//public class RoleConfiguration : IEntityTypeConfiguration<Role>
//{
//    public void Configure(EntityTypeBuilder<Role> builder)
//    {
//        builder.Property(p => p.Id)
//            .IsRequired();

//        builder.Property(p => p.Name)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.Description)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.IsActive)
//            .IsRequired()
//            .HasDefaultValue(false);
//    }
//}

//public class DishConfiguration : IEntityTypeConfiguration<Dish>
//{
//    public void Configure(EntityTypeBuilder<Dish> builder)
//    {
//        builder.Property(p => p.Id)
//            .IsRequired();

//        builder.Property(p => p.Name)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.Description)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.Price)
//            .IsRequired();

//        builder.Property(p => p.IsActive)
//            .IsRequired()
//            .HasDefaultValue(false);

//        builder.HasOne(p => p.DishType);
//    }
//}

//public class DishTypeConfiguration : IEntityTypeConfiguration<DishType>
//{
//    public void Configure(EntityTypeBuilder<DishType> builder)
//    {
//        builder.Property(p => p.Id)
//            .IsRequired();

//        builder.Property(p => p.Name)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.Description)
//            .IsRequired()
//            .HasMaxLength(50);

//        builder.Property(p => p.IsActive)
//            .IsRequired()
//            .HasDefaultValue(false);
//    }
//}

