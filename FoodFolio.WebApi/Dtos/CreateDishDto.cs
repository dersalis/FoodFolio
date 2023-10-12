namespace FoodFolio.WebApi.Dtos;

public class CreateDishDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? PhotoUrl { get; set; }
    public int DishTypeId { get; set; }
    public int? CreatedById { get; set; }
    public DateTime? CreatedDate { get; set; }
}

