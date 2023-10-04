using System;
using Microsoft.AspNetCore.Mvc;

namespace FoodFolio.WebApi.Controllers;

[Route("api/dish")]
[ApiController]
public class DishController : ControllerBase
{
	public DishController()
	{
	}

	[HttpGet]
	public ActionResult GetAll()
	{
		return Ok();
	}

    [HttpGet("currentday")]
    public ActionResult GetCurrentDay()
    {
        return Ok();
    }

    [HttpGet("currentweek")]
    public ActionResult GetCurrentWeek()
    {
        return Ok();
    }

    [HttpPost]
    public ActionResult Create()
    {
        return Created("", null);
    }

    [HttpPut]
    public ActionResult Update()
    {
        return NoContent();
    }

    [HttpDelete]
    public ActionResult Delete()
    {
        return NoContent();
    }
}

