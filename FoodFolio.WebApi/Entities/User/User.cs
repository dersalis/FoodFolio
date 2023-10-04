namespace FoodFolio.WebApi.Entities;

public class User : EntityBase
{
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string PasswordHash { get; set; }
    public string LastPasswordHash { get; set; }
    public bool IsActive { get; set; }

    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
}

