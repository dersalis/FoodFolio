namespace FoodFolio.WebApi.Dtos;

public class DishDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? PhotoFileUrl { get; set; }
    public string? PhotoFileBase64 { get; set; }
    public DateTime ServingDate { get; set; }
    public int DishTypeId { get; set; }
    public int? CreatedById { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
}