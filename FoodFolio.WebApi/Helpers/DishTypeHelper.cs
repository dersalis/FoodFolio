using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Helpers;

public class DishTypeHelper
{
    public static async Task<DishType> GetDishTypeById(FoodFolioDbContext dbContext, int id)
    {
        DishType dishType = await dbContext.DishTypes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dishType is null) throw new NotFoundException($"Dish type (id = {id}) not found");

        return dishType;
    }
}

