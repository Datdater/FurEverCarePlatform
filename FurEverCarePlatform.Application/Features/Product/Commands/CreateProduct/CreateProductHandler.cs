//using FurEverCarePlatform.Application.Features.Product.Validations;

//namespace FurEverCarePlatform.Application.Features.Product.Commands.CreateProduct;

//public class CreateProductHandler(IUnitOfWork unitOfWork)
//    : IRequestHandler<CreateProductCommand, Guid>
//{
//    public async Task<Guid> Handle(
//        CreateProductCommand request,
//        CancellationToken cancellationToken
//    )
//    {
//        var validator = new CreateProductValidator();
//        var validationResult = await validator.ValidateAsync(request);
//        if (!validationResult.IsValid)
//        {
//            throw new BadRequestException(validationResult.ToString(), validationResult);
//        }
//        try
//        {
//            await unitOfWork.BeginTransactionAsync();

//            // Tạo sản phẩm
//            var product = new Domain.Entities.Product()
//            {
//                Name = request.Name,
//                ProductCategoryId = request.ProductCategoryId,
//                IsActive = request.IsActive,
//                ProductDescription = request.ProductDescription,
//                Height = request.Height,
//                Length = request.Length,
//                Weight = request.Weight,
//                Width = request.Width,
//                BrandId = request.BrandId,
//                StoreId = request.StoreId,
//            };

//            var productTypes = new List<Domain.Entities.ProductType>();
//            var productTypeDetailsDict =
//                new Dictionary<string, Domain.Entities.ProductTypeDetail>();

//            // Duyệt từng loại sản phẩm (productType)
//            foreach (var productType in request.ProductTypes)
//            {
//                var newProductType = new Domain.Entities.ProductType { Name = productType.Name };
//                productTypes.Add(newProductType);

//                var productTypeDetails = new List<Domain.Entities.ProductTypeDetail>();

//                foreach (var productTypeDetail in productType.ProductTypeDetails)
//                {
//                    var newProductTypeDetail = new Domain.Entities.ProductTypeDetail
//                    {
//                        Name = productTypeDetail.Name,
//                    };

//                    productTypeDetails.Add(newProductTypeDetail);
//                    productTypeDetailsDict[newProductTypeDetail.Name] = newProductTypeDetail; // Lưu vào dictionary để tra cứu
//                }

//                newProductType.ProductTypeDetails = productTypeDetails;
//            }

//            product.ProductTypes = productTypes;

//            await unitOfWork.GetRepository<Domain.Entities.Product>().InsertAsync(product);
//            await unitOfWork.SaveAsync(); // Lưu `Product` và `ProductType` trước
            
//            product.ProductImages = request.ProductImages.Select(
//                x => new Domain.Entities.ProductImages
//                {
//                    URL = x.URL,
//                    ProductId = product.Id,
//                }
//            ).ToList(); ;

//            var productPrices = new List<Domain.Entities.ProductPrice>();

//            foreach (var productPrice in request.ProductPrices)
//            {
//                if (
//                    productTypeDetailsDict.TryGetValue(
//                        productPrice.ProductTypeDetails1,
//                        out var productTypeDetail1
//                    )
//                )
//                {
//                    var newProductPrice = new Domain.Entities.ProductPrice();
//                    if (
//                        productPrice.ProductTypeDetails2 != null
//                        && productTypeDetailsDict.TryGetValue(
//                            productPrice.ProductTypeDetails2,
//                            out var productTypeDetail2
//                        )
//                    )
//                    {
//                        newProductPrice = new Domain.Entities.ProductPrice
//                        {
//                            Price = productPrice.Price,
//                            Inventory = productPrice.Inventory,
//                            ProductTypeDetails1 = productTypeDetail1.Id, // Liên kết bằng ID
//                            ProductTypeDetails2 = productTypeDetail2.Id,
//                        };
//                    }
//                    else
//                    {
//                        newProductPrice = new Domain.Entities.ProductPrice
//                        {
//                            Price = productPrice.Price,
//                            Inventory = productPrice.Inventory,
//                            ProductTypeDetails1 = productTypeDetail1.Id, // Liên kết bằng ID
//                            ProductTypeDetails2 = null,
//                        };
//                    }

//                    productPrices.Add(newProductPrice);
//                }
//                else
//                {
//                    throw new NotFoundException(
//                        nameof(ProductTypeDetail),
//                        $"{productPrice.ProductTypeDetails1} or {productPrice.ProductTypeDetails2}"
//                    );
//                }
//            }

//            await unitOfWork
//                .GetRepository<Domain.Entities.ProductPrice>()
//                .AddRangeAsync(productPrices);
//            await unitOfWork.SaveAsync(); // Lưu `ProductPrice`

//            await unitOfWork.CommitTransactionAsync();
//            return product.Id;
//        }
//        catch (System.Exception)
//        {
//            await unitOfWork.RollbackTransactionAsync();
//            throw new BadRequestException("Create product failed");
//        }
//    }
//}
