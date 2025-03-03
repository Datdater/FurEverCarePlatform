namespace FurEverCarePlatform.Domain.Entities;

public class Pet : BaseEntity
{
    public required string Name { get; set; }
    public bool? PetType { get; set; }
    public DateTime? Age { get; set; }

    public Guid? BreedId { get; set; }
    public string? SpecialRequirement { get; set; }
    public required Guid UserId { get; set; }
    public string? Color { get; set; }

    public virtual AppUser AppUser { get; set; }
    public virtual Breed Breed { get; set; }
    public ICollection<HealthDetail> HealthDetails { get; set; }
    public ICollection<BookingDetail> BookingDetails { get; set; }
}
