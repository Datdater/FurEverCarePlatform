using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetServiceReviews
{
    public class GetServiceReviewHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetServiceReviewQuery, List<ServiceReviewDto>>
    {
        public async Task<List<ServiceReviewDto>> Handle(GetServiceReviewQuery request, CancellationToken cancellationToken)
        {
            var serviceReviews = await unitOfWork
                          .GetRepository<ServiceReview>()
                          .GetQueryable()
                          .Include(x => x.AppUser)
                          .Where(x => x.PetServiceId == request.Id)
                          .Select(x => new ServiceReviewDto
                          {
                              Id = x.Id,
                              UserId = x.AppUserId,
                              UserName = x.AppUser.UserName,
                              Rating = x.Rating,
                              Comment = x.Comment,
                          })
                          .ToListAsync(cancellationToken);
            return serviceReviews;
        }
    }
}
