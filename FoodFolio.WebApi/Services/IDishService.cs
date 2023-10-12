using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Services
{
    public interface IDishService
    {
        Task<int> CreateAsync(CreateDishDto dish);
        Task DeleteAsync(int id);
        Task<IEnumerable<DishDto>> GetAllAsync();
        Task<PagedResultDto<DishDto>> GetAllAsync(QueryDto query);
        Task<DishDto> GetByIdAsync(int id);
        Task<IEnumerable<DishDto>> GetCurrentDayAsync();
        Task<IEnumerable<DishDto>> GetCurrentWeekAsync();
        Task UpdateAsync(int id, UpdateDishDto dish);
    }
}