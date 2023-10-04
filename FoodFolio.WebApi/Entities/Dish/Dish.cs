namespace FoodFolio.WebApi.Entities;

public class Dish : MetricBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? PhotoUrl { get; set; }

    public int DishTypeId { get; set; }
    public virtual DishType DishType { get; set; }
}

