using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.Products.DTOs;

namespace FurEverCarePlatform.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    [Required]
    public Guid ProductCategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool? IsActive { get; set; }

    [MaxLength(1000)]
    public string? ProductDescription { get; set; }
    public decimal Weight { get; set; }
    public int Length { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    [Required]
    public Guid BrandId { get; set; }

    [Required]
    public Guid StoreId { get; set; }

    // Navigation properties
    public List<ProductTypeDTO> ProductTypes { get; set; }
    public List<ProductImagesDTO> ProductImages { get; set; }
    public List<ProductPricesDTO> ProductPrices { get; set; }
}
