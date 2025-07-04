using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Guid>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public float BasePrice { get; set; }

    public decimal Weight { get; set; }
    public decimal Width { get; set; }

    public decimal Length { get; set; }

    public decimal Height { get; set; }

    [Required]
    public Guid StoreId { get; set; }

    // Collections for related entities
    public List<UpdateProductVariantDTO> Variants { get; set; } = new();
    public List<UpdateProductImageDTO> Images { get; set; } = new();
}

public class UpdateProductVariantDTO
{
    public Guid? Id { get; set; } // For existing variants
    public Dictionary<string, object> Attributes { get; set; } = new();
    public float Price { get; set; }
    public int Stock { get; set; }
}

public class UpdateProductImageDTO
{
    public Guid? Id { get; set; } // For existing images
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}