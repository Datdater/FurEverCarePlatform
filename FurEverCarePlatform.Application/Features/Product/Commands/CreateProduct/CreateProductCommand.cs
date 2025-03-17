using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    [Required]
    public Guid ProductCategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool? IsActive { get; set; }

    [MaxLength(50)]
    public string? ProductCode { get; set; }

    public int? Views { get; set; }

    [Required]
    public Guid BrandId { get; set; }

    [Required]
    public Guid StoreId { get; set; }

    // Navigation properties
    public List<ProductTypeDTO> ProductTypes { get; set; }
    public List<ProductPricesDTO> ProductPrices { get; set; }
}
