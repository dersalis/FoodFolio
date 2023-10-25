using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Helpers;

public class UserHelper
{
    public static async Task<User> GetUserById(FoodFolioDbContext dbContext, int id)
    {
        User user = await dbContext.Users
            .FirstOrDefaultAsync(d => d.Id == id);

        if (user is null) throw new NotFoundException($"User type (id = {id}) not found");

        return user;
    }

    public static async Task CheckUserExistAsync(FoodFolioDbContext dbContext, string email)
    {
        User user = await dbContext.Users
            .FirstOrDefaultAsync(d => d.Email == email);

        if (user is not null) throw new NotFoundException($"User (email = {email}) exist");
    }
}

