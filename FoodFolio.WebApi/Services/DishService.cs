using System;
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
    private readonly FoodFolioDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _hostEnvironment;

    private const string DIRECTORY = "/wwwroot/";

    public DishService(
        FoodFolioDbContext dbContext,
        IMapper mapper,
        IWebHostEnvironment environment
        )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _hostEnvironment = environment;
    }

    public async Task<IEnumerable<DishDto>> GetAllAsync()
    {
        IEnumerable<Dish> dishes = _dbContext.Dishes;

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<PagedResultDto<DishDto>> GetAllAsync(QueryDto query)
    {
        var queryResult = _dbContext.Dishes
            .Where(d => query.SearchPhrase == null && (d.Name.ToLower().Contains(query.SearchPhrase.ToLower()) && d.Description.ToLower().Contains(query.SearchPhrase.ToLower())));


        if (!string.IsNullOrEmpty(query.SortBy))
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

    public async Task<DishDto> GetByIdAsync(int id, HttpRequest request)
    {
        Dish dish = await GetDishById(id);
        //dish.PhotoUrl = Path.Combine(_hostEnvironment.WebRootPath, dish.PhotoUrl);
        //dish.PhotoUrl = $"{context.Request.Scheme}";
        //var host = $"{context.Request.Scheme}://{context.Request.Host}";
        dish.PhotoUrl = $"{request.Scheme}://{request.Host.ToString()}{dish.PhotoUrl}";
        return _mapper.Map<DishDto>(dish);
    }

    public async Task<IEnumerable<DishDto>> GetCurrentDayAsync()
    {
        DateTime fromDate = DateTime.Now.Date;
        DateTime toDate = fromDate.AddDays(1);

        IEnumerable<Dish> dishes = await _dbContext.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<IEnumerable<DishDto>> GetCurrentWeekAsync()
    {
        DateTime fromDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        DateTime toDate = fromDate.AddDays(5);

        IEnumerable<Dish> dishes = await _dbContext.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

    public async Task<int> CreateAsync(CreateDishDto dish)
    {
        Dish newDish = _mapper.Map<Dish>(dish);
        newDish.IsActive = true;
        newDish.PhotoUrl = UploadFile(dish.File);
        //newDish.CreatedBy = null; // TODO: Dodać uzytkownika
        newDish.CreatedDate = DateTime.Now;

        await _dbContext.Dishes.AddAsync(newDish);
        await _dbContext.SaveChangesAsync();

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

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Dish dishToDelete = await GetDishById(id);

        _dbContext.Dishes.Remove(dishToDelete);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Dish> GetDishById(int id)
    {
        Dish dish = await _dbContext.Dishes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dish is null) throw new NotFoundException($"Dish (id = {id}) not found");

        return dish;
    }

    private async Task<DishType> GetDishTypeById(int id)
    {
        DishType dishType = await _dbContext.DishTypes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dishType is null) throw new NotFoundException($"Dish type (id = {id}) not found");

        return dishType;
    }

    private string UploadFile(IFormFile file)
    {
        if (file is null || file.Length <= 0) throw new BadRequestException("Zły plik");

        string currentDirectory = Directory.GetCurrentDirectory();
        string fileName = $"{Guid.NewGuid().ToString()}.{file.FileName.Substring(file.FileName.Length - 3)}";
        string filePath = DIRECTORY + fileName;
        string fullPath = currentDirectory + filePath;

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return filePath;
    }
}

