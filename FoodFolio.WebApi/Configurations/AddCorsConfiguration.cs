using System;
namespace FoodFolio.WebApi.Configurations
{
	public static class AddCorsConfiguration
	{
        public static void AddCorsServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials();
            }));
        }
    }
}

