using AutoMapper;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;

namespace FoodFolio.WebApi.MappingProfiles;

public class DishMappingProfile : Profile
{
	public DishMappingProfile()
	{
		CreateMap<Dish, CreateDishDto>();
	}
}

