using AutoMapper;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;

namespace FoodFolio.WebApi.MappingProfiles;

public class DishTypeProfileMapping : Profile
{
	public DishTypeProfileMapping()
	{
		CreateMap<DishType, DishTypeDto>();
        CreateMap<CreateDishTypeDto, DishType>();
    }
}

