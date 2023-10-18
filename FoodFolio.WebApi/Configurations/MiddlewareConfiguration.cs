using FoodFolio.WebApi.Middleware;

namespace FoodFolio.WebApi.Configurations;

public static class MiddlewareConfiguration
{
	public static void AddMiddlewareServices(this WebApplicationBuilder builder)
	{
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
    }
}

