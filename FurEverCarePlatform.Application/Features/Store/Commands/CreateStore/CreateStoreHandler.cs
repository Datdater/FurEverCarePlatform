namespace FurEverCarePlatform.Application.Features.Store.Commands.CreateStore;

public class CreateStoreHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateStoreCommand, Guid>
{
    public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateStoreValidator();
        var validationResult = await validator.ValidateAsync(request, CancellationToken.None);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.ToString(), validationResult);
        }
        try
        {
            // Tạo cửa hàng
            await unitOfWork.BeginTransactionAsync();
            var store = new Domain.Entities.Store()
            {
                AddressId = request.AddressId,
                UserId = Guid.Parse("862C5EF9-2F7A-446D-B7E5-3FC3D121600D"),
                Name = request.Name,
                Hotline = request.Hotline,
                LogoUrl = request.LogoUrl,
                BannerUrl = request.BannerUrl,
                BusinessType = request.BusinessType,
                BusinessAddressProvince = request.BusinessAddressProvince,
                BusinessAddressDistrict = request.BusinessAddressDistrict,
                BusinessAddressWard = request.BusinessAddressWard,
                BusinessAddressStreet = request.BusinessAddressStreet,
                FaxEmail = request.FaxEmail,
                FaxCode = request.FaxCode,
                FrontIdentityCardUrl = request.FrontIdentityCardUrl,
                BackIdentityCardUrl = request.BackIdentityCardUrl,
            };
            var newStore = await unitOfWork
                .GetRepository<Domain.Entities.Store>()
                .InsertAsync(store);
            await unitOfWork.SaveAsync();
            await unitOfWork.CommitTransactionAsync();
            return newStore.Id;
        }
        catch (System.Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new BadRequestException($"Error in creating store: {ex.Message}");
        }
    }
}
