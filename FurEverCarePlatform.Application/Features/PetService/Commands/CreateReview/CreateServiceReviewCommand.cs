using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreateReview
{
    public class CreateServiceReviewCommand : IRequest
    {
        public Guid PetServiceId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Guid? BookingId { get; set; }
        public CreateServiceReviewCommand(Guid petServiceId, string review, int rating, Guid? bookingId = null)
        {
            PetServiceId = petServiceId;
            Comment = review;
            Rating = rating;
            BookingId = bookingId;
        }
    }
}
