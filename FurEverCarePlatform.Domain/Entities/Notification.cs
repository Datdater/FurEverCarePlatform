using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid? UserId { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public string ReturnUrl { get; set; }
    
    public Guid? StoreId { get; set; }  

    //navigation
    [ForeignKey("UserId")]
    public virtual AppUser? AppUser { get; set; }

    public virtual Store? Store { get; set; }

}
