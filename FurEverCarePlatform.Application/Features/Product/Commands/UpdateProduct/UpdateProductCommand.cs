using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid ProductCategoryId { get; set; }
    public string Name { get; set; }
    public bool? IsActive { get; set; }
    public decimal Weight { get; set; }
    public int Lenght { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public string ProductDescription { get; set; }
    public int? Views { get; set; }
    public Guid BrandId { get; set; }
    public Guid StoreId { get; set; }
    public List<ProductTypeDTO> ProductTypes { get; set; }
    public List<ProductImagesDTO> ProductImages { get; set; }
    public List<ProductPricesDTO> ProductPrices { get; set; }
}
