using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Services
{
    public interface IDishTypeService
    {
        Task<int> CreateAsync(CreateDishTypeDto dishType);
        Task<IEnumerable<DishTypeDto>> GetAllAsync();
    }
}