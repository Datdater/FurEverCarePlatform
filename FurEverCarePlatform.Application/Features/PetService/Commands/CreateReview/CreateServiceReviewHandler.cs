using FurEverCarePlatform.Application.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreateReview
{
    public class CreateServiceReviewHandler(IUnitOfWork unitOfWork, IClaimService claimService) : IRequestHandler<CreateServiceReviewCommand>
    {
        public async Task Handle(CreateServiceReviewCommand request, CancellationToken cancellationToken)
        {
            if (request.Rating < 1 || request.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }
            var userId = claimService.GetCurrentUser;
            var serviceReview = new ServiceReview
            {
                PetServiceId = request.PetServiceId,
                Comment = request.Comment,
                Rating = request.Rating,
                BookingId = request.BookingId,
                AppUserId = userId 
            };
            var service = await unitOfWork.GetRepository<Domain.Entities.PetService>().GetByIdAsync(request.PetServiceId);
            if (service == null)
            {
                throw new ArgumentException("Service not found.");
            }
            
            service.AddReview(serviceReview);

            unitOfWork.GetRepository<Domain.Entities.PetService>().Update(service);

            await unitOfWork.GetRepository<ServiceReview>().InsertAsync(serviceReview);
            await unitOfWork.SaveAsync();
        }
    }
}
