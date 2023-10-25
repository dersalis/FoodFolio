using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Helpers;

public class DishHelper
{
    public static async Task<Dish> GetDishById(FoodFolioDbContext dbContext, int id)
    {
        Dish dish = await dbContext.Dishes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dish is null) throw new NotFoundException($"Dish (id = {id}) not found");

        return dish;
    }
}

