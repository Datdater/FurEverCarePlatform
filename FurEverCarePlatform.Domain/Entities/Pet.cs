namespace FurEverCarePlatform.Domain.Entities;

public class Pet : BaseEntity
{
    public required string Name { get; set; }
    public bool? PetType { get; set; }
    public DateTime? Dob { get; set; }
    public float? Weight { get; set; }

    public string? SpecialRequirement { get; set; }
    public string? Image { get; set; }
    public required Guid AppUserId { get; set; }
    public string? Color { get; set; }

    public virtual AppUser AppUser { get; set; }
    public ICollection<BookingDetail> BookingDetails { get; set; }
}
