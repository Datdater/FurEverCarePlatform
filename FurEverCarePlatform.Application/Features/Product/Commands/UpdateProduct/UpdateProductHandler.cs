namespace FurEverCarePlatform.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateProductCommand, Guid>
{
    public async Task<Guid> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.ToString(), validationResult);
        }
        try
        {
            await unitOfWork.BeginTransactionAsync();

            var productRepository = unitOfWork.GetRepository<Domain.Entities.Product>();
            var product = await productRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.Id,
                "ProductTypes"
            );

            if (product == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
            }

            product.Name = request.Name;
            product.ProductCategoryId = request.ProductCategoryId;
            product.IsActive = request.IsActive;
            product.ProductCode = request.ProductCode;
            product.Views = request.Views;
            product.BrandId = request.BrandId;
            product.StoreId = request.StoreId;

            // Update product prices
            var productPrices = new List<Domain.Entities.ProductPrice>();
            var productPriceRepository = unitOfWork.GetRepository<Domain.Entities.ProductPrice>();
            var productPricesToDelete = unitOfWork.ProductRepository.GetProductPrices(request.Id);
            foreach (var productPrice in productPricesToDelete)
            {
                productPriceRepository.Delete(productPrice);
            }

            // Delete existing product types
            var productTypeRepository = unitOfWork.GetRepository<Domain.Entities.ProductType>();

            foreach (var productType in product.ProductTypes.ToList())
            {
                productTypeRepository.Delete(productType);
            }
            await unitOfWork.SaveAsync();

            // Update product types
            var productTypes = new List<Domain.Entities.ProductType>();
            var productTypeDetailsDict =
                new Dictionary<string, Domain.Entities.ProductTypeDetail>();
            foreach (var productType in request.ProductTypes)
            {
                var newProductType = new Domain.Entities.ProductType { Name = productType.Name };
                productTypes.Add(newProductType);

                var productTypeDetails = new List<Domain.Entities.ProductTypeDetail>();

                foreach (var productTypeDetail in productType.ProductTypeDetails)
                {
                    var newProductTypeDetail = new Domain.Entities.ProductTypeDetail
                    {
                        Name = productTypeDetail.Name,
                    };

                    productTypeDetails.Add(newProductTypeDetail);
                    productTypeDetailsDict[newProductTypeDetail.Name] = newProductTypeDetail;
                }

                newProductType.ProductTypeDetails = productTypeDetails;
            }

            product.ProductTypes = productTypes;
            productRepository.Update(product);
            await unitOfWork.SaveAsync();

            foreach (var productPrice in request.ProductPrices)
            {
                if (
                    productTypeDetailsDict.TryGetValue(
                        productPrice.ProductTypeDetails1,
                        out var productTypeDetail1
                    )
                )
                {
                    var newProductPrice = new Domain.Entities.ProductPrice();
                    if (
                        productPrice.ProductTypeDetails2 != null
                        && productTypeDetailsDict.TryGetValue(
                            productPrice.ProductTypeDetails2,
                            out var productTypeDetail2
                        )
                    )
                    {
                        newProductPrice = new Domain.Entities.ProductPrice
                        {
                            Price = productPrice.Price,
                            Inventory = productPrice.Inventory,
                            ProductTypeDetails1 = productTypeDetail1.Id, // Liên kết bằng ID
                            ProductTypeDetails2 = productTypeDetail2.Id,
                        };
                    }
                    else
                    {
                        newProductPrice = new Domain.Entities.ProductPrice
                        {
                            Price = productPrice.Price,
                            Inventory = productPrice.Inventory,
                            ProductTypeDetails1 = productTypeDetail1.Id, // Liên kết bằng ID
                            ProductTypeDetails2 = null,
                        };
                    }

                    productPrices.Add(newProductPrice);
                }
                else
                {
                    throw new NotFoundException(
                        nameof(ProductTypeDetail),
                        $"{productPrice.ProductTypeDetails1} or {productPrice.ProductTypeDetails2}"
                    );
                }
            }

            await productPriceRepository.AddRangeAsync(productPrices);
            await unitOfWork.SaveAsync();

            await unitOfWork.CommitTransactionAsync();
            return product.Id;
        }
        catch (System.Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new BadRequestException("Update product failed");
        }
    }
}
