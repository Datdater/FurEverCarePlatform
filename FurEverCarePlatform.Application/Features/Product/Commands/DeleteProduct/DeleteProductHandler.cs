using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }
        product.IsDeleted = true;
        unitOfWork.ProductRepository.Update(product);
        await unitOfWork.SaveAsync();
    }
}
