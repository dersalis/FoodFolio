using System.Security.Claims;

namespace FoodFolio.WebApi.Services
{
    public interface IUserContextService
    {
        //ClaimsPrincipal User { get; }
        //int? GetUserId { get; }
        int GetUserId();
    }
}