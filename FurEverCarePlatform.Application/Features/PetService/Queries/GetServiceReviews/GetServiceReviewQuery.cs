using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetServiceReviews
{
    public class GetServiceReviewQuery : IRequest<List<ServiceReviewDto>>
    {
        public Guid Id { get; set; }
    }
    public class ServiceReviewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
