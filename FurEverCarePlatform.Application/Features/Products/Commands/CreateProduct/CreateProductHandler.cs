using System.Text.Json;

namespace FurEverCarePlatform.Application.Features.Products.Commands.CreateProduct;

public class CreateProductHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var validator = new CreateProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.ToString(), validationResult);
        }

        try
        {
            await unitOfWork.BeginTransactionAsync();

            var product = new Domain.Entities.Product
            {
                CategoryId = request.CategoryId,
                StoreId = request.StoreId,
                Name = request.Name,
                Description = request.Description,
                BasePrice = request.BasePrice,
                Weight = request.Weight,
                Length = request.Length,
                Height = request.Height,
                Sold = 0,
                StarAverage = 5.0,
                ReviewCount = 0,
                TotalRating = 0
            };

            if (request.Variants?.Any() == true)
            {
                foreach (var variantDto in request.Variants)
                {
                    var variant = new Domain.Entities.ProductVariant
                    {
                        ProductId = product.Id,
                        Attributes = JsonDocument.Parse(JsonSerializer.Serialize(variantDto.Attributes)),
                        Price = variantDto.Price,
                        Stock = variantDto.Stock
                    };

                    product.Variants.Add(variant);
                }
            }

            if (request.Images?.Any() == true)
            {
                var hasMainImage = request.Images.Any(img => img.IsMain);
                if (!hasMainImage && request.Images.Count > 0)
                {
                    request.Images[0].IsMain = true;
                }

                foreach (var imageDto in request.Images)
                {
                    var image = new Domain.Entities.ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = imageDto.ImageUrl,
                        IsMain = imageDto.IsMain
                    };

                    product.Images.Add(image);
                }
            }

            await unitOfWork.GetRepository<Domain.Entities.Product>().InsertAsync(product);
            await unitOfWork.SaveAsync();

            await unitOfWork.CommitTransactionAsync();
            return product.Id;
        }
        catch (SystemException ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new BadRequestException($"Create product failed: {ex.Message}");
        }
    }
}