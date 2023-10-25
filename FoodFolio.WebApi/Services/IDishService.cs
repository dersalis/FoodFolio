using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Services
{
    public interface IDishService
    {
        Task<int> CreateAsync(CreateDishDto dish);
        Task DeleteAsync(int id);
        Task<IEnumerable<DishDto>> GetAllAsync(string host);
        Task<PagedResultDto<DishDto>> GetAllAsync(string host, QueryDto query);
        //Task<DishDto> GetByIdAsync(int id);
        Task<DishDto> GetByIdAsync(int id, string host);
        Task<IEnumerable<DishDto>> GetCurrentDayAsync(string host);
        Task<IEnumerable<DishDto>> GetCurrentWeekAsync(string host);
        Task UpdateAsync(int id, UpdateDishDto dish);
    }
}