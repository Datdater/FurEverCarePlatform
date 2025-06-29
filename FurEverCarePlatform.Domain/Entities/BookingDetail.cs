using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;

public class BookingDetail : BaseEntity
{
    public Guid PetServiceDetailId { get; set; }
    public Guid PetId { get; set; }
    public required DateTime BookingTime { get; set; }
    public float RealAmount { get; set; }
    public bool IsMeasured { get; set; }
    public float RawAmount { get; set; }
    public float? PetWeight { get; set; }
    public string? Hair { get; set; }
    public Guid? AssignedUserId { get; set; }

    //Navigation
    public virtual Booking? Booking { get; set; }
    public virtual Pet Pet { get; set; } = null!;
    public virtual AppUser? AssignedUser { get; set; }
    [ForeignKey("PetServiceDetailId")]
    public virtual PetServiceDetail PetServiceDetail { get; set; } = null!;
}
