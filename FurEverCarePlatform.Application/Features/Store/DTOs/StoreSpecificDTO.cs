namespace FurEverCarePlatform.Application.Features.Store.DTOs;

public class StoreSpecificDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required string Hotline { get; set; }

    public string? LogoUrl { get; set; }

    public string? BannerUrl { get; set; }

    public string? BusinessType { get; set; }

    public string? BusinessAddressProvince { get; set; }

    public string? BusinessAddressDistrict { get; set; }

    public string? BusinessAddressWard { get; set; }

    public string? BusinessAddressStreet { get; set; }

    public string? FaxEmail { get; set; }

    public string? FaxCode { get; set; }

    public string? FrontIdentityCardUrl { get; set; }

    public string? BackIdentityCardUrl { get; set; }
}
