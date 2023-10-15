using System.Linq.Expressions;
using AutoMapper;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Extensions;
using FoodFolio.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Services;

public class DishService : IDishService
{
    private readonly FoodFolioDbContext _dbContext;
    private readonly IMapper _mapper;

    //private const string DIRECTORY = "/wwwroot/pictures/";

    public DishService(
        FoodFolioDbContext dbContext,
        IMapper mapper
        )
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishDto>> GetAllAsync(string host)
    {
        IEnumerable<Dish> dishes = _dbContext.Dishes;

        return dishes.Select(d => new DishDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            PhotoFile = FileHelper.GetFilePath(host, d.PhotoFile),
            ServingDate = d.ServingDate,
            DishTypeId = d.DishTypeId,
            CreatedById = d.CreatedById,
            CreatedDate = d.CreatedDate,
            ModifiedById = d.ModifiedById,
            ModifiedDate = d.ModifiedDate,
        });
    }

    public async Task<PagedResultDto<DishDto>> GetAllAsync(string host, QueryDto query)
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

        //var dishesDto = _mapper.Map<IEnumerable<DishDto>>(dishes);

        var dishesDto = dishes.Select(d => new DishDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            PhotoFile = FileHelper.GetFilePath(host, d.PhotoFile),
            ServingDate = d.ServingDate,
            DishTypeId = d.DishTypeId,
            CreatedById = d.CreatedById,
            CreatedDate = d.CreatedDate,
            ModifiedById = d.ModifiedById,
            ModifiedDate = d.ModifiedDate,
        });

        var result = new PagedResultDto<DishDto>(dishesDto, totalItemsCount, query.PageSize, query.PageNumber);
        return result;
    }

    public async Task<DishDto> GetByIdAsync(int id, string host)
    {
        Dish dish = await DishHelper.GetDishById(_dbContext, id);
        dish.PhotoFile = FileHelper.GetFilePath(host, dish.PhotoFile);

        return _mapper.Map<DishDto>(dish);
    }

    public async Task<IEnumerable<DishDto>> GetCurrentDayAsync(string host)
    {
        DateTime fromDate = DateTime.Now.Date;
        DateTime toDate = fromDate.AddDays(1);

        IEnumerable<Dish> dishes = await _dbContext.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate)
            .ToListAsync();

        //return _mapper.Map<IEnumerable<DishDto>>(dishes);

        return dishes.Select(d => new DishDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            PhotoFile = FileHelper.GetFilePath(host, d.PhotoFile),
            ServingDate = d.ServingDate,
            DishTypeId = d.DishTypeId,
            CreatedById = d.CreatedById,
            CreatedDate = d.CreatedDate,
            ModifiedById = d.ModifiedById,
            ModifiedDate = d.ModifiedDate,
        });
    }

    public async Task<IEnumerable<DishDto>> GetCurrentWeekAsync(string host)
    {
        DateTime fromDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        DateTime toDate = fromDate.AddDays(5);

        IEnumerable<Dish> dishes = await _dbContext.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate)
            .ToListAsync();

        //return _mapper.Map<IEnumerable<DishDto>>(dishes);

        return dishes.Select(d => new DishDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            PhotoFile = FileHelper.GetFilePath(host, d.PhotoFile),
            ServingDate = d.ServingDate,
            DishTypeId = d.DishTypeId,
            CreatedById = d.CreatedById,
            CreatedDate = d.CreatedDate,
            ModifiedById = d.ModifiedById,
            ModifiedDate = d.ModifiedDate,
        });
    }

    public async Task<int> CreateAsync(CreateDishDto dish)
    {
        Dish newDish = _mapper.Map<Dish>(dish);
        newDish.IsActive = true;
        newDish.PhotoFile = FileHelper.UploadFile(dish.File);
        //newDish.CreatedBy = null; // TODO: Dodać uzytkownika
        newDish.CreatedDate = DateTime.Now;

        await _dbContext.Dishes.AddAsync(newDish);
        await _dbContext.SaveChangesAsync();

        return newDish.Id;
    }

    public async Task UpdateAsync(int id, UpdateDishDto dish)
    {
        Dish dishToUpdate = await DishHelper.GetDishById(_dbContext, id);
        DishType dishType = await DishTypeHelper.GetDishTypeById(_dbContext, dish.DishTypeId);

        dishToUpdate.Name = dish.Name;
        dishToUpdate.Description = dish.Description;
        dishToUpdate.Price = dish.Price;
        if (dish.File is not null)
        {
            dishToUpdate.PhotoFile = FileHelper.UploadFile(dish.File);
        }
        dishToUpdate.DishType = dishType;
        //dishToUpdate.ModifiedBy = null; // TODO: Dodać uzytkownika
        dishToUpdate.ModifiedDate = DateTime.Now;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Dish dishToDelete = await DishHelper.GetDishById(_dbContext, id);

        _dbContext.Dishes.Remove(dishToDelete);
        await _dbContext.SaveChangesAsync();
    }
}

