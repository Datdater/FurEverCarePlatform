using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities;

public class Report : BaseEntity
{
    [Required]
    public string Issue { get; set; }

    [Required]
    [MaxLength(255)]
    public string Content { get; set; }
    public string? Attachment { get; set; }
    public bool Status { get; set; }

    [Required]
    public Guid UserId;

    //Navigation
    public AppUser AppUser { get; set; }
}