using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Helpers;

public class RoleHelper
{
    public static async Task<Role> GetRoleByNameAsync(FoodFolioDbContext dbContext, string roleName)
    {
        Role role = await dbContext.Roles
            .FirstOrDefaultAsync(d => d.Name == roleName);

        if (role is null) throw new NotFoundException($"Role (name = {roleName}) not found");

        return role;
    }
}

