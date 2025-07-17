using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Application.Features.Products.Commands.CreateProductReviews
{
    public class CreateProductCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<CreateProductReviewCommand>
    {
        public async Task Handle(
            CreateProductReviewCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Rating < 1 || request.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            var productReview = new ProductReviews
            {
                ProductId = request.ProductId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow,
                AppUserId = request.GetAppUserId(),
                OrderDetailId = request.OrderDetailId,
            };

            // Get the product to update its rating statistics
            var product = await unitOfWork.GetRepository<Product>().GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new ArgumentException("Product not found.");
            }

            // Insert the review
            await unitOfWork.GetRepository<ProductReviews>().InsertAsync(productReview);

            // Update product rating statistics
            product.AddReview(productReview);
            unitOfWork.GetRepository<Product>().Update(product);

            await unitOfWork.SaveAsync();
        }
    }
}
