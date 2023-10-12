﻿namespace FoodFolio.WebApi.Dtos;

public class UpdateDishDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? PhotoUrl { get; set; }
    public int DishTypeId { get; set; }
    public int? ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
