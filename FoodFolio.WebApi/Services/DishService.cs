using System;
using AutoMapper;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using FoodFolio.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodFolio.WebApi.Services;

public class DishService
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

	public async Task<IEnumerable<DishDto>> GetAll()
	{
		IEnumerable<Dish> dishes = _context.Dishes;

		return _mapper.Map<IEnumerable<DishDto>>(dishes);
	}

	public async Task<DishDto> GetById(int id)
	{
		Dish dish = await GetDishById(id);

		return _mapper.Map<DishDto>(dish);
	}

	public async Task<IEnumerable<DishDto>> GetCurrentDay()
	{
		DateTime fromDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        DateTime toDate = fromDate.AddDays(5);

		IEnumerable<Dish> dishes = _context.Dishes
			.Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate);

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

	public async Task<IEnumerable<DishDto>> GetCurrentWeek()
	{
        DateTime fromDate = DateTime.Now.Date;
        DateTime toDate = fromDate.AddDays(1);

        IEnumerable<Dish> dishes = _context.Dishes
            .Where(d => d.ServingDate >= fromDate && d.ServingDate <= toDate);

        return _mapper.Map<IEnumerable<DishDto>>(dishes);
    }

	public async Task<int> Create(CreateDishDto dish)
	{
		Dish newDish = _mapper.Map<Dish>(dish);
		await _context.SaveChangesAsync();

		return newDish.Id;
	}

	public async Task Update(int id, UpdateDishDto dish)
	{
        Dish dishToUpdate = await GetDishById(id);

		dishToUpdate.Name = dish.Name;
		dishToUpdate.Description = dish.Description;
		dishToUpdate.Price = dish.Price;
		dishToUpdate.PhotoUrl = dish.PhotoUrl;
		dishToUpdate.DishType = null; //TODO: UStawić - skończyć

		await _context.SaveChangesAsync();
	}

	public Task Delete(int id)
	{
		return null;
	}

	private async Task<Dish> GetDishById(int id)
	{
		Dish dish = await _context.Dishes
			.FirstOrDefaultAsync(d => d.Id == id);

		if (dish is null) throw new NotFoundException($"Dish (id = {id}) not found");

		return dish;
	}
}

