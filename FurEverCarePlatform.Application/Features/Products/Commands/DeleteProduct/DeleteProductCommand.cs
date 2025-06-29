using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}
