using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Services;
using Microsoft.AspNetCore.Identity;

namespace FoodFolio.WebApi.Configurations;

public static class ServicesConfiguration
{
	public static void AddServices(this WebApplicationBuilder builder)
	{
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IDishService, DishService>();
        builder.Services.AddScoped<IDishTypeService, DishTypeService>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddScoped<IUserContextService, UserContextService>();
    }
}

