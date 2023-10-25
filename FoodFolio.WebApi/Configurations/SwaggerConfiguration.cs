using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace FoodFolio.WebApi.Configurations;

public static class SwaggerConfiguration
{
	public static void AddSwaggerService(this WebApplicationBuilder builder)
	{
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("V1", new OpenApiInfo
            {
                Version = "V1",
                Title = "FoodFolio API",
                Description = " WebAPI ;)",
                Contact = new OpenApiContact
                {
                    Name = "Damian Ruta",
                    Email = "hello@damianruta.pl",
                    Url = new Uri("https://twitter.com/damianruta"),
                },
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                //Type = SecuritySchemeType.ApiKey,
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddFluentValidationRulesToSwagger();
    }
}

