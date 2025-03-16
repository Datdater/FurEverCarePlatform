using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;
[Table("Stores")]
public class Store : BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }

    public required Guid AddressId { get; set; }

    [MaxLength(20)]
    public required string Hotline { get; set; }

    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [MaxLength(500)]
    public string? BannerUrl { get; set; }

    [MaxLength(255)]
    public string? BusinessType { get; set; }

    [MaxLength(255)]
    public string? BusinessAddressProvince { get; set; }

    [MaxLength(255)]
    public string? BusinessAddressDistrict { get; set; }

    [MaxLength(255)]
    public string? BusinessAddressWard { get; set; }

    [MaxLength(255)]
    public string? BusinessAddressStreet { get; set; }

    [MaxLength(255)]
    public string? FaxEmail { get; set; }

    [MaxLength(50)]
    public string? FaxCode { get; set; }

    [MaxLength(500)]
    public string? FrontIdentityCardUrl { get; set; }

    [MaxLength(500)]
    public string? BackIdentityCardUrl { get; set; }


    //navigation    
    public ICollection<Promotion> Promotions { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<PetService> PetServices { get; set; }
    public virtual Address? Address { get; set; }
    public virtual AppUser? AppUser { get; set; }
}
