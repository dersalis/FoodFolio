using System.Linq.Expressions;
using AutoMapper;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using FoodFolio.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Services;

public class DishService : IDishService
{
    private readonly FoodFolioDbContext _context;
    private readonly IMapper _mapper;

    public DishService(
        FoodFolioDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishDto>> GetAllAsync()
    {
        IEnumerable<Dish> dishes = _context.Dishes;

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<PagedResultDto<DishDto>> GetAllAsync(QueryDto query)
    {
        var queryResult = _context.Dishes
            .Where(d => query.SearchPhrase == null && (d.Name.ToLower().Contains(query.SearchPhrase.ToLower()) && d.Description.ToLower().Contains(query.SearchPhrase.ToLower())));


        if(!string.IsNullOrEmpty(query.SortBy))
        {
            var columnSelectors = new Dictionary<string, Expression<Func<Dish, object>>>
            {
                { nameof(Dish.Name), d => d.Name},
                { nameof(Dish.Description), d => d.Description },
                { nameof(Dish.Price), d => d.Price }
            };

            var selectedColumn = columnSelectors[query.SortBy];

            queryResult = query.SortDirection == Enums.SortDirection.ASC
                ? queryResult.OrderBy(selectedColumn)
                : queryResult.OrderByDescending(selectedColumn);
        }

        var dishes = queryResult
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToList();

        int totalItemsCount = queryResult.Count();

        var dishesDto = _mapper.Map<IEnumerable<DishDto>>(dishes);

        var result = new PagedResultDto<DishDto>(dishesDto, totalItemsCount, query.PageSize, query.PageNumber);

        return result;
    }

    public async Task<DishDto> GetByIdAsync(int id)
    {
        Dish dish = await GetDishById(id);

        return _mapper.Map<DishDto>(dish);
    }

    public async Task<IEnumerable<DishDto>> GetCurrentDayAsync()
    {
        DateTime fromDate = DateTime.Now.Date;
        DateTime toDate = fromDate.AddDays(1);

        IEnumerable<Dish> dishes = await _context.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<IEnumerable<DishDto>> GetCurrentWeekAsync()
    {
        DateTime fromDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        DateTime toDate = fromDate.AddDays(5);

        IEnumerable<Dish> dishes = await _context.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<int> CreateAsync(CreateDishDto dish)
    {
        Dish newDish = _mapper.Map<Dish>(dish);
        newDish.IsActive = true;
        //newDish.CreatedBy = null; // TODO: Dodać uzytkownika
        newDish.CreatedDate = DateTime.Now;

        await _context.Dishes.AddAsync(newDish);
        await _context.SaveChangesAsync();

        return newDish.Id;
    }

    public async Task UpdateAsync(int id, UpdateDishDto dish)
    {
        Dish dishToUpdate = await GetDishById(id);
        DishType dishType = await GetDishTypeById(dish.DishTypeId);

        dishToUpdate.Name = dish.Name;
        dishToUpdate.Description = dish.Description;
        dishToUpdate.Price = dish.Price;
        dishToUpdate.PhotoUrl = dish.PhotoUrl; //TODO: Zrobić zapisywanie plików
        dishToUpdate.DishType = dishType;
        //dishToUpdate.ModifiedBy = null; // TODO: Dodać uzytkownika
        dishToUpdate.ModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Dish dishToDelete = await GetDishById(id);

        _context.Dishes.Remove(dishToDelete);
        await _context.SaveChangesAsync();
    }

    private async Task<Dish> GetDishById(int id)
    {
        Dish dish = await _context.Dishes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dish is null) throw new NotFoundException($"Dish (id = {id}) not found");

        return dish;
    }

    private async Task<DishType> GetDishTypeById(int id)
    {
        DishType dishType = await _context.DishTypes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dishType is null) throw new NotFoundException($"Dish type (id = {id}) not found");

        return dishType;
    }
}

