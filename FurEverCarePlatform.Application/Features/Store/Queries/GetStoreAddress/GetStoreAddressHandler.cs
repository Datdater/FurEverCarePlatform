using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetStoreAddress;

public class GetStoreAddressHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetStoreAddressQuery, IEnumerable<StoreAddressDTO>>
{
    public async Task<IEnumerable<StoreAddressDTO>> Handle(
        GetStoreAddressQuery request,
        CancellationToken cancellationToken
    )
    {
        var userAddresses = await unitOfWork
            .GetRepository<Address>()
            .GetAllAsync(
                filter: x => x.UserId == Guid.Parse("862C5EF9-2F7A-446D-B7E5-3FC3D121600D"),
                includeProperties: "Store"
            );

        var userAddressDtos = userAddresses
            .Where(address => address.Store == null)
            .Select(address => new StoreAddressDTO()
            {
                Id = address.Id,
                AddressFullPath = $"{address.Street}, {address.City}, {address.Province}",
            });
        return userAddressDtos;
    }
}
