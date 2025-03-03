namespace FurEverCarePlatform.Domain.Entities;

public class Feedback : BaseEntity
{
    public string? Detail { get; set; }
    public int? Rating { get; set; }
    public string? Attachment { get; set; }
    public bool Status { get; set; }
    public required Guid UserId { get; set; }

    //Navigation
    public virtual AppUser AppUser { get; set; }
    public virtual Booking Booking { get; set; }
    public virtual OrderDetail OrderDetail { get; set; }
}
