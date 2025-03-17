namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService
{
    public class GetPetServiceQuery : IRequest<PetServiceDto>
    {
        public Guid Id { get; set; }
    }
}
