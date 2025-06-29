using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetProductReviews
{
    public class GetProductReviewsQuery : IRequest<List<ProductReviewDto>>
    {
        public Guid ProductId { get; set; }
    }

    public class ProductReviewDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
