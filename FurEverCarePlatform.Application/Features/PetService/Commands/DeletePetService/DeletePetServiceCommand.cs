namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetService
{
    public class DeletePetServiceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
