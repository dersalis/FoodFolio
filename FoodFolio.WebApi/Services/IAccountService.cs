using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto login);
        Task RegisterUserAsync(RegisterUserDto registerUser);
    }
}