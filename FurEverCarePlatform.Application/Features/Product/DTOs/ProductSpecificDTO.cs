using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.DTOs;

public class ProductSpecificDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string? ProductDescription { get; set; }
    public string? BrandName { get; set; }
    public string? StoreName { get; set; }
    public string? CategoryName { get; set; }
    public List<ProductTypeDTO> ProductTypes { get; set; }
    public List<ProductPricesDTO> ProductPrices { get; set; }
    public List<ProductImagesDTO> ProductImages { get; set; }

}
