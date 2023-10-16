namespace FoodFolio.WebApi.Dtos;

public class DishTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? CreatedById { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

