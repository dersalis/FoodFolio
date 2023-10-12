﻿using System;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodFolio.WebApi.Controllers;

[Route("api/dish")]
[ApiController]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
	{
        _dishService = dishService;
	}

	[HttpGet]
	public async Task<ActionResult<DishDto>> GetAll()
	{
        IEnumerable<DishDto> result = await _dishService.GetAllAsync();
		return Ok(result);
	}

    [HttpGet("paged")]
    public async Task<ActionResult<PagedResultDto<DishDto>>> GetAll([FromBody] QueryDto query)
    {
        PagedResultDto<DishDto> result = await _dishService.GetAllAsync(query);
        return Ok(result);
    }

    [HttpGet("byid/{id:int}")]
    public async Task<ActionResult<DishDto>> GetByIdAll(int id)
    {
        DishDto result = await _dishService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("currentday")]
    public async Task<ActionResult<DishDto>> GetCurrentDay()
    {
        IEnumerable<DishDto> result = await _dishService.GetCurrentDayAsync();
        return Ok(result);
    }

    [HttpGet("currentweek")]
    public async Task<ActionResult<DishDto>> GetCurrentWeek()
    {
        IEnumerable<DishDto> result = await _dishService.GetCurrentWeekAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateDishDto dish)
    {
        int dishId = await _dishService.CreateAsync(dish);
        return Created($"api/dish/{dishId}", null);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateDishDto dish)
    {
        await _dishService.UpdateAsync(id, dish);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _dishService.DeleteAsync(id);
        return NoContent();
    }
}

