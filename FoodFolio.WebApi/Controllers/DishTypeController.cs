using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodFolio.WebApi.Controllers;

[Route("api/dishtype")]
[ApiController]
public class DishTypeController : ControllerBase
{
	private readonly IDishTypeService _dishTypeService;

    public DishTypeController(IDishTypeService dishTypeService)
	{
        _dishTypeService = dishTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<DishDto>> GetAll()
    {
        IEnumerable<DishTypeDto> result = await _dishTypeService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateDishTypeDto dishType)
    {
        int dishTypeId = await _dishTypeService.CreateAsync(dishType);
        return Created($"api/dishtype/{dishTypeId}", null);
    }
}