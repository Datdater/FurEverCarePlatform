namespace FurEverCarePlatform.Domain.Entities;

public class Booking : BaseEntity
{
    public required DateTime BookingTime { get; set; }
    public string? Description { get; set; }

    public required Guid UserId { get; set; }
    public required decimal RawAmount { get; set; }
    public required decimal TotalAmount { get; set; }
    public Guid? DeliveryId { get; set; }
    public Guid? UserAddressId { get; set; }
    public float Distance { get; set; }
    public Guid? PromotionId { get; set; }

    public Guid? FeedbackId { get; set; }

    //navigation
    public virtual Delivery Delivery { get; set; }
    public virtual AppUser AppUser { get; set; }
    public virtual Address Address { get; set; }
    public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    public virtual Promotion Promotion { get; set; }
    public virtual Feedback? Feedback { get; set; }
    public virtual Payment Payment { get; set; }
}
