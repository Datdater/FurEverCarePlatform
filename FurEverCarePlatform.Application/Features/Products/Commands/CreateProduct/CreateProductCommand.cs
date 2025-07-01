using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public float BasePrice { get; set; }

    public decimal Weight { get; set; }

    public decimal Length { get; set; }

    public decimal Height { get; set; }

    [Required]
    public Guid StoreId { get; set; }

    public List<CreateProductVariantDTO> Variants { get; set; } = new();
    public List<CreateProductImageDTO> Images { get; set; } = new();
}

public class CreateProductVariantDTO
{
    public Dictionary<string, object> Attributes { get; set; } = new();
    public float Price { get; set; }
    public int Stock { get; set; }
}

public class CreateProductImageDTO
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}