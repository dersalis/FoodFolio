namespace FoodFolio.WebApi.Entities;

public static class Seeder
{
	public static void Seed(this WebApplication webApp)
	{
        using (var scope = webApp.Services.CreateScope())
        {
            using (var context = scope.ServiceProvider.GetRequiredService<FoodFolioDbContext>())
            {
                try
                {
                    if (context.Database.CanConnect())
                    {
                        if (!context.Roles.Any())
                        {
                            var roles = CreateRoles();
                            context.Roles.AddRange(roles);
                            context.SaveChanges();
                        }
                        if (!context.Users.Any())
                        {
                            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                            var users = CreateAdminUser(adminRole);
                            context.Users.AddRange(users);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    // throw;
                }
            }
        }
    }

    private static IEnumerable<User> CreateAdminUser(Role role)
    {
        return new List<User>()
        {
            new User()
            {
                Email = "admin@admin.pl",
                FirstName = "admin",
                LastName = "Admian",
                PasswordHash = "", //TODO: Dodać
                LastPasswordHash = "",
                Role = role
            }
        };
    }

    private static IEnumerable<Role> CreateRoles()
    {
        return new List<Role>()
        {
            new Role()
            {
                Name = "Admin",
                Description = "Admin user"
            },
            new Role()
            {
                Name = "User",
                Description = "Default user"
            }
        };
    }
}

