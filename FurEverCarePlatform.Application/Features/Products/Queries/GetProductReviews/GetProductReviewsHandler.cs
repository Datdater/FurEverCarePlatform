using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetProductReviews
{
    public class GetProductReviewsHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetProductReviewsQuery, List<ProductReviewDto>>
    {
        public async Task<List<ProductReviewDto>> Handle(
            GetProductReviewsQuery request,
            CancellationToken cancellationToken
        )
        {
            var productReviews = await unitOfWork
                .GetRepository<ProductReviews>()
                .GetQueryable()
                .Include(x => x.AppUser)
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => new ProductReviewDto
                {
                    Id = x.Id,
                    UserId = x.AppUserId,
                    UserName = x.AppUser.UserName,
                    Rating = x.Rating,
                    Comment = x.Comment,
                    CreatedAt = x.CreatedAt,
                })
                .ToListAsync(cancellationToken);
            return productReviews;
        }
    }
}
