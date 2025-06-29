using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Products.DTOs;

public class ProductTypeDTO
{
    public string Name { get; set; }
    public List<ProductTypeDetailsDTO> ProductTypeDetails { get; set; }
}
