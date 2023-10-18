using FluentValidation;
using FluentValidation.AspNetCore;

namespace FoodFolio.WebApi.Configurations;

public static class FluentValidatorConfiguration
{
	public static void AddFluentValidatorService(this WebApplicationBuilder builder)
	{
        builder.Services.AddValidatorsFromAssemblyContaining<Program>(lifetime: ServiceLifetime.Scoped);
        builder.Services.AddFluentValidationAutoValidation();
    }
}

