﻿using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetStoreAddress;

public class GetStoreAddressHandler(IUnitOfWork unitOfWork, IClaimService claimService)
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
                filter: x => x.UserId == claimService.GetCurrentUser,
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
