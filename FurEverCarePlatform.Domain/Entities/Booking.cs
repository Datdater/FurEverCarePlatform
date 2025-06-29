using FurEverCarePlatform.Domain.Enums;

namespace FurEverCarePlatform.Domain.Entities;

public class Booking : BaseEntity
{
    public BookingStatus Status { get; set; } = BookingStatus.Booked;
    public required DateTime BookingTime { get; set; }
    public string? Description { get; set; }

    public Guid AppUserId { get; set; }
    public required float RawAmount { get; set; }
    public required float TotalAmount { get; set; }
    public required string Code { get; set; }
    public Guid? PromotionId { get; set; }

    public Guid? FeedbackId { get; set; }
    public Guid StoreId { get; set; }

    //navigation
    public virtual AppUser AppUser { get; set; }
    public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    public virtual Promotion Promotion { get; set; }
    public virtual Feedback? Feedback { get; set; }
    public virtual Payment Payment { get; set; }
    public virtual Store Store { get; set; }
}
