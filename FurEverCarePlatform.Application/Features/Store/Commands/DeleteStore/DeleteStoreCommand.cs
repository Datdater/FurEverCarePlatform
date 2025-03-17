namespace FurEverCarePlatform.Application.Features.Store.Commands.DeleteStore;

public class DeleteStoreCommand : IRequest
{
    public Guid Id { get; set; }
}
