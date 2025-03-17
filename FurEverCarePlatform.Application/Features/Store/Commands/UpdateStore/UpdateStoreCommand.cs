using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Application.Features.Store.Commands.UpdateStore;

public class UpdateStoreCommand : IRequest<Guid>
{
    [Required]
    public required Guid Id { get; set; }

    //[Required]
    //public required Guid AddressId { get; set; }

    [Required]
    [MaxLength(20)]
    public required string Name { get; set; }

    [Required]
    public required string Hotline { get; set; }

    public string LogoUrl { get; set; } = string.Empty;
    public string BannerUrl { get; set; } = string.Empty;

    [Required]
    public required string BusinessType { get; set; }

    [Required]
    public required string BusinessAddressProvince { get; set; }

    [Required]
    public required string BusinessAddressDistrict { get; set; }

    [Required]
    public required string BusinessAddressWard { get; set; }

    [Required]
    public required string BusinessAddressStreet { get; set; }

    [Required]
    public required string FaxEmail { get; set; }

    [Required]
    public required string FaxCode { get; set; }

    [Required]
    public required string FrontIdentityCardUrl { get; set; }

    [Required]
    public required string BackIdentityCardUrl { get; set; }
}
