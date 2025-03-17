namespace FurEverCarePlatform.Domain.Entities;

using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string? Name { get; set; }
    public DateTime? CreationDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? ModificationDate { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DeleteDate { get; set; }
    public Guid? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }

    //navigation
    public virtual ICollection<Store> Stores { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }
    public virtual ICollection<Pet> Pets { get; set; }
    public virtual ICollection<Address> Address { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; }
    public virtual ICollection<Feedback> Feedback { get; set; }
    public virtual ICollection<ChatMessage> ChatMessage { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
}
