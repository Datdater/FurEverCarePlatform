using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
