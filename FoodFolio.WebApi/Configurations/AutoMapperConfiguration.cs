namespace FoodFolio.WebApi.Configurations;

public static class AutoMapperConfiguration
{
	public static void AddAutoMapperServices(this WebApplicationBuilder builder)
	{
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}

