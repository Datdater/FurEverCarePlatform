namespace FurEverCarePlatform.Domain.Entities;

public class Notification : BaseEntity
{
    public required Guid UserId { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public string ReturnUrl { get; set; }
    public Guid? FromUserId { get; set; }

    //navigation
    public virtual AppUser AppUser { get; set; }
    public virtual AppUser FromAppUserId { get; set; }

}
