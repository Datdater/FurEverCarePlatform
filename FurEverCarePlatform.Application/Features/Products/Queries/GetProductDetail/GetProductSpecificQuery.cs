using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.Products.DTOs;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetProductDetail;

public class GetProductSpecificQuery : IRequest<ProductSpecificDTO>
{
    public Guid Id { get; set; }
}
