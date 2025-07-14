using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Products.Commands.CreateProductReviews
{
    public class CreateProductReviewCommand : IRequest
    {
        public Guid ProductId { get; set; }
        private Guid AppUserId;
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public Guid GetAppUserId()
        {
            return AppUserId;
        }

        public void SetAppUserId(Guid appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
