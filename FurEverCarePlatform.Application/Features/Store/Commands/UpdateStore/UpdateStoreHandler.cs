namespace FurEverCarePlatform.Application.Features.Store.Commands.UpdateStore;

public class UpdateStoreHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStoreCommand, Guid>
{
    public async Task<Guid> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateStoreValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.ToString(), validationResult);
        }
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var storeRepository = unitOfWork.GetRepository<Domain.Entities.Store>();
            var store = await storeRepository.GetByIdAsync(request.Id);
            if (store == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);
            }
            store.Name = request.Name;
            //store.AddressId = request.AddressId;
            store.Hotline = request.Hotline;
            store.LogoUrl = request.LogoUrl;
            store.BannerUrl = request.BannerUrl;
            store.BusinessType = request.BusinessType;
            store.BusinessAddressProvince = request.BusinessAddressProvince;
            store.BusinessAddressDistrict = request.BusinessAddressDistrict;
            store.BusinessAddressWard = request.BusinessAddressWard;
            store.BusinessAddressStreet = request.BusinessAddressStreet;
            store.FaxEmail = request.FaxEmail;
            store.FaxCode = request.FaxCode;
            store.FrontIdentityCardUrl = request.FrontIdentityCardUrl;
            store.BackIdentityCardUrl = request.BackIdentityCardUrl;
            storeRepository.Update(store);
            await unitOfWork.SaveAsync();
            await unitOfWork.CommitTransactionAsync();
            return store.Id;
        }
        catch (System.Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new BadRequestException("Update product failed");
        }
    }
}
