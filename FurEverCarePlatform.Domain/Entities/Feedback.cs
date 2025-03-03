namespace FurEverCarePlatform.Domain.Entities;

public class Feedback : BaseEntity
{
    public string? Detail { get; set; }
    public int? Rating { get; set; }
    public string? Attachment { get; set; }
    public bool Status { get; set; }
    public required Guid UserId { get; set; }

    //Navigation
    public AppUser AppUser { get; set; }
}
