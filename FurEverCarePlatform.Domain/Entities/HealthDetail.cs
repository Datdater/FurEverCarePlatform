using FurEverCarePlatform.Domain.Entities;

public class HealthDetail : BaseEntity
{
    [Required]
    public float Weight { get; set; }

    [Required]
    public Guid PetId { get; set; }
    [Required]
    public DateTime MeasureDate { get; set; }

    public virtual Pet Pet { get; set; }

    
}
