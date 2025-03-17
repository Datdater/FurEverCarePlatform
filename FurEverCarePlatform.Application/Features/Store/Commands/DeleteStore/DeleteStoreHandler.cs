namespace FurEverCarePlatform.Application.Features.Store.Commands.DeleteStore;

public class DeleteStoreHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteStoreCommand>
{
    public async Task Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
    {
        var storeRepository = unitOfWork.GetRepository<Domain.Entities.Store>();
        var store = await storeRepository.GetByIdAsync(request.Id);
        if (store == null)
        {
            throw new NotFoundException(nameof(Store), request.Id);
        }
        store.IsDeleted = true;
        storeRepository.Update(store);
        await unitOfWork.SaveAsync();
    }
}
