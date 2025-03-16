using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Queries.GetProductDetail;

public class GetProductSpecificQuery : IRequest<ProductSpecificDTO>
{
    public Guid Id { get; set; }
}
