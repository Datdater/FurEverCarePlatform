namespace FurEverCarePlatform.Domain.Entities;

public class BookingDetail : BaseEntity
{
    public Guid BookingServiceId { get; set; }
    public Guid PetId { get; set; }
    public required DateTime BookingTime { get; set; }
    public decimal RealAmount { get; set; }
    public bool IsMeasured { get; set; }
    public decimal RawAmount { get; set; }
    public float? PetWeight { get; set; }
    public string? Hair { get; set; }
    public Guid? ComboId { get; set; }
    public Guid? AssignedUserId { get; set; }

    //Navigation
    public virtual Booking? Booking { get; set; }
    public virtual Pet? Pet { get; set; }
    public virtual AppUser? AssignedUser { get; set; }
    public virtual Combo? Combo { get; set; }

    public virtual ICollection<BookingDetailService>? BookingDetailServices { get; set; }
}
