namespace FoodFolio.WebApi.Dtos;

public class CreateDishDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ServingDate { get; set; }
    //public string? PhotoUrl { get; set; }
    public IFormFile? File { get; set; }
    public int DishTypeId { get; set; }
}

