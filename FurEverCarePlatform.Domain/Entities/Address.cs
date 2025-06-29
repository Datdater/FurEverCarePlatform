using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities;

public class Address : BaseEntity
{
    [MaxLength(20)]
    public required string Phone { get; set; }

    [MaxLength(255)]
    public required string Street { get; set; }

    [MaxLength(100)]
    public required string City { get; set; }
    public double? CoordinateX { get; set; }
    public double? CoordinateY { get; set; }

    [MaxLength(100)]
    public string? Province { get; set; }
    public int? PostalCode { get; set; }

    [Required]
    public Guid AppUserId { get; set; }

    //navigation
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }
    public virtual AppUser AppUser { get; set; }
}
