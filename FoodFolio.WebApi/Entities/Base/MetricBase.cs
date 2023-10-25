namespace FoodFolio.WebApi.Entities;

public class MetricBase : EntityBase
{
    public virtual User? CreatedBy { get; set; }
    public int? CreatedById { get; set; }
    public DateTime? CreatedDate { get; set; }

    public virtual User? ModifiedBy { get; set; }
    public int? ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

