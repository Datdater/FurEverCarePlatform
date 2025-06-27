using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;

public class ChatMessage : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [MaxLength(255)]
    public string Content { get; set; }

    public bool IsRead { get; set; }
    public Guid ToUserId { get; set; }
    public string Type { get; set; }
    public bool IsSend { get; set; }
    public string Reaction { get; set; }
    public bool IsUserDeleted { get; set; }
    public bool IdToUserDeleted { get; set; }
    public string AttachmentName { get; set; }
    public string FieldAttachmentUrl { get; set; }
    [ForeignKey("UserId")]
    public virtual AppUser AppUser { get; set; }
    [ForeignKey("ToUserId")]
    public virtual AppUser ToAppUser { get; set; }
}
