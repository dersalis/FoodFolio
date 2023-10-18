using System.Security.Claims;
using FoodFolio.WebApi.Exceptions;

namespace FoodFolio.WebApi.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

    //public int? UserId => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

    public int GetUserId()
    {
        ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;

        if (user is null) throw new NotFoundException($"User type not found");

        return int.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}

