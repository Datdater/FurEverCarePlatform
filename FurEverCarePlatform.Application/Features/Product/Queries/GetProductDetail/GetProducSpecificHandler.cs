﻿using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Queries.GetProductDetail;

public class GetProducSpecificHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductSpecificQuery, ProductSpecificDTO>
{
    public async Task<ProductSpecificDTO> Handle(GetProductSpecificQuery request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id, "ProductCategory,Store,ProductBrand");
        var productSpecific = mapper.Map<ProductSpecificDTO>(product);
        var productTypesListRaw = await unitOfWork.GetRepository<ProductType>().GetAllAsync(x => x.ProductId == request.Id, "ProductTypeDetails");
        var productTypeList = mapper.Map<List<ProductTypeDTO>>(productTypesListRaw);
        productSpecific.ProductTypes = productTypeList;
        var productPricesListRaw = unitOfWork.ProductRepository.GetProductPrices(request.Id);
        var productPricesList = productPricesListRaw.Select(x => new ProductPricesDTO()
        {
            Inventory = x.Inventory,
            Price = x.Price,
            ProductTypeDetails1 = x.ProductType1.Name,
            ProductTypeDetails2 = x.ProductType2?.Name
        }).ToList();
        productSpecific.ProductPrices = productPricesList;
        return productSpecific;
    }
}
