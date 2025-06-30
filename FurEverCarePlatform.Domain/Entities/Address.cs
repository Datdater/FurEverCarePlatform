using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities;

public class Address : BaseEntity
{
    public string? PhoneNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Ward { get; set; }
    public string? District { get; set; }
    public bool IsDefault { get; set; } = false;
    public string Name { get; set; }

    [Required]
    public Guid AppUserId { get; set; }

    //navigation
    public virtual ICollection<Order> Orders { get; set; }
    public virtual AppUser AppUser { get; set; }
}
