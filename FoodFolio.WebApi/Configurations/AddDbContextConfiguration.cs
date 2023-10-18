using FoodFolio.WebApi.Entities;

namespace FoodFolio.WebApi.Configurations;

public static class AddDbContextConfiguration
{
	public static void AddDbContextServices(this WebApplicationBuilder builder)
	{
        builder.Services.AddDbContext<FoodFolioDbContext>();
    }
}

