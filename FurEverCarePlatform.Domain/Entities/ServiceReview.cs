namespace FurEverCarePlatform.Domain.Entities;

public class ServiceReview : BaseEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public required Guid UserId { get; set; }
    public Guid? PetServiceId { get; set; }
    public Guid? BookingId { get; set; }

    //Navigation
    public virtual AppUser AppUser { get; set; }
    public Booking? Booking { get; set; }
    public PetService? PetService { get; set; }
}
