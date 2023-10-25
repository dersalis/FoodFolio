using Microsoft.EntityFrameworkCore;

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

