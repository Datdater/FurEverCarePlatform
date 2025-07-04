using FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using System.Linq;
using System.Text.Json;

namespace FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductCommand, Guid>
{
    public async Task<Guid> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

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
                includeProperties: "Variants,Images"
            );

            if (product == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
            }

            product.CategoryId = request.CategoryId;
            product.StoreId = request.StoreId;
            product.Name = request.Name;
            product.Description = request.Description;
            product.BasePrice = request.BasePrice;
            product.Weight = request.Weight;
            product.Length = request.Length;
            product.Height = request.Height;
            product.Width = request.Width;

            // Update variants
            await UpdateProductVariants(product, request.Variants);

            // Update images
            await UpdateProductImages(product, request.Images);

            productRepository.Update(product);
            await unitOfWork.SaveAsync();

            await unitOfWork.CommitTransactionAsync();
            return product.Id;
        }
        catch (SystemException ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new BadRequestException($"Update product failed: {ex.Message}");
        }
    }

    private async Task UpdateProductVariants(Domain.Entities.Product product, List<UpdateProductVariantDTO> variantDtos)
    {
        var variantRepository = unitOfWork.GetRepository<Domain.Entities.ProductVariant>();

        var existingVariants = product.Variants.ToList();
        var requestVariantIds = variantDtos.Where(v => v.Id.HasValue).Select(v => v.Id.Value).ToList();

        var variantsToDelete = existingVariants.Where(v => !requestVariantIds.Contains(v.Id)).ToList();
        foreach (var variant in variantsToDelete)
        {
            variantRepository.Delete(variant);
            product.Variants.Remove(variant);
        }

        foreach (var variantDto in variantDtos)
        {
            if (variantDto.Id.HasValue)
            {
                var existingVariant = existingVariants.FirstOrDefault(v => v.Id == variantDto.Id.Value);
                if (existingVariant != null)
                {
                    existingVariant.Attributes = JsonDocument.Parse(JsonSerializer.Serialize(variantDto.Attributes));
                    existingVariant.Price = variantDto.Price;
                    existingVariant.Stock = variantDto.Stock;
                }
            }
            else
            {
                var newVariant = new Domain.Entities.ProductVariant
                {
                    ProductId = product.Id,
                    Attributes = JsonDocument.Parse(JsonSerializer.Serialize(variantDto.Attributes)),
                    Price = variantDto.Price,
                    Stock = variantDto.Stock
                };

                product.Variants.Add(newVariant);
            }
        }
    }

    private async Task UpdateProductImages(Domain.Entities.Product product, List<UpdateProductImageDTO> imageDtos)
    {
        var imageRepository = unitOfWork.GetRepository<Domain.Entities.ProductImage>();

        var existingImages = product.Images.ToList();
        var requestImageIds = imageDtos.Where(i => i.Id.HasValue).Select(i => i.Id.Value).ToList();

        var imagesToDelete = existingImages.Where(i => !requestImageIds.Contains(i.Id)).ToList();
        foreach (var image in imagesToDelete)
        {
            imageRepository.Delete(image);
            product.Images.Remove(image);
        }

        var hasMainImage = imageDtos.Any(img => img.IsMain);
        if (!hasMainImage && imageDtos.Count > 0)
        {
            imageDtos[0].IsMain = true;
        }

        foreach (var existingImage in existingImages)
        {
            existingImage.IsMain = false;
        }

        // Update or add images
        foreach (var imageDto in imageDtos)
        {
            if (imageDto.Id.HasValue)
            {
                // Update existing image
                var existingImage = existingImages.FirstOrDefault(i => i.Id == imageDto.Id.Value);
                if (existingImage != null)
                {
                    existingImage.ImageUrl = imageDto.ImageUrl;
                    existingImage.IsMain = imageDto.IsMain;
                }
            }
            else
            {
                // Add new image
                var newImage = new Domain.Entities.ProductImage
                {
                    ProductId = product.Id,
                    ImageUrl = imageDto.ImageUrl,
                    IsMain = imageDto.IsMain
                };

                product.Images.Add(newImage);
            }
        }
    }
}