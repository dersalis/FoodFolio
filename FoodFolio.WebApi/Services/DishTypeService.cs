using AutoMapper;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;

namespace FoodFolio.WebApi.Services;

public class DishTypeService : IDishTypeService
{
    private readonly FoodFolioDbContext _dbContext;
    private readonly IMapper _mapper;

    public DishTypeService(
        FoodFolioDbContext dbContext,
        IMapper mapper
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishTypeDto>> GetAllAsync()
    {
        IEnumerable<DishType> dishTypes = _dbContext.DishTypes;

        return _mapper.Map<IEnumerable<DishTypeDto>>(dishTypes);
    }

    public async Task<int> CreateAsync(CreateDishTypeDto dishType)
    {
        DishType newDishType = _mapper.Map<DishType>(dishType);
        newDishType.IsActive = true;
        //newDishType.CreatedBy = null;
        newDishType.CreatedDate = DateTime.Now;

        await _dbContext.DishTypes.AddAsync(newDishType);
        await _dbContext.SaveChangesAsync();

        return newDishType.Id;
    }
}