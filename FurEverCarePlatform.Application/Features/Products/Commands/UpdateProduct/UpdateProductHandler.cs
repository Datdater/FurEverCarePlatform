using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductHandler(IUnitOfWork unitOfWork, IClaimService claimService)
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
            var userId = claimService.GetCurrentUser;

            var store = await unitOfWork.GetRepository<Domain.Entities.Store>()
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.AppUserId == userId);

            if (store == null)
                throw new System.Exception("Not found store with this user");

            // Validate CategoryId exists
            var categoryExists = await unitOfWork.GetRepository<Domain.Entities.ProductCategory>()
                .GetQueryable()
                .AnyAsync(c => c.Id == request.CategoryId);

            if (!categoryExists)
            {
                throw new BadRequestException($"Category with ID {request.CategoryId} does not exist");
            }

            var productRepository = unitOfWork.GetRepository<Domain.Entities.Product>();
            var product = await productRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.Id,
                includeProperties: "Variants,Images"
            );

            if (product == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
            }

            // Check for duplicate product name in the same store
            var duplicateProduct = await productRepository.GetQueryable()
                .AnyAsync(p => p.Name == request.Name && p.StoreId == store.Id && p.Id != request.Id);

            if (duplicateProduct)
            {
                throw new BadRequestException("A product with this name already exists in your store");
            }

            // Validate string lengths (adjust limits based on your schema)
            if (request.Name?.Length > 255)
            {
                throw new BadRequestException("Product name is too long (max 255 characters)");
            }

            if (request.Description?.Length > 2000)
            {
                throw new BadRequestException("Product description is too long (max 2000 characters)");
            }

            // Update product properties
            product.CategoryId = request.CategoryId;
            product.StoreId = store.Id;
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
        catch (DbUpdateException dbEx)
        {
            await unitOfWork.RollbackTransactionAsync();

            // Extract the actual database error
            var innerException = dbEx.InnerException?.Message ?? dbEx.Message;

            // Common PostgreSQL constraint error patterns
            if (innerException.Contains("duplicate key"))
            {
                throw new BadRequestException("A record with this information already exists");
            }
            else if (innerException.Contains("foreign key"))
            {
                throw new BadRequestException("Referenced data does not exist");
            }
            else if (innerException.Contains("check constraint"))
            {
                throw new BadRequestException("Data violates business rules");
            }
            else if (innerException.Contains("not null"))
            {
                throw new BadRequestException("Required field is missing");
            }

            throw new BadRequestException($"Database update failed: {innerException}");
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

        foreach (var variantDto in variantDtos)
        {
            if (variantDto.Id.HasValue)
            {
                // Update existing variant
                var existingVariant = product.Variants.FirstOrDefault(v => v.Id == variantDto.Id.Value);
                if (existingVariant != null)
                {
                    existingVariant.Attributes = JsonDocument.Parse(JsonSerializer.Serialize(variantDto.Attributes));
                    existingVariant.Price = variantDto.Price;
                    existingVariant.Stock = variantDto.Stock;
                    variantRepository.Update(existingVariant);
                }
            }
            else
            {
                // Add new variant
                var newVariant = new Domain.Entities.ProductVariant
                {
                    ProductId = product.Id,
                    Attributes = JsonDocument.Parse(JsonSerializer.Serialize(variantDto.Attributes)),
                    Price = variantDto.Price,
                    Stock = variantDto.Stock
                };

                await variantRepository.InsertAsync(newVariant);
            }
        }
    }

    private async Task UpdateProductImages(Domain.Entities.Product product, List<UpdateProductImageDTO> imageDtos)
    {
        var imageRepository = unitOfWork.GetRepository<Domain.Entities.ProductImage>();

        // Reset all images to not main first
        foreach (var existingImage in product.Images)
        {
            existingImage.IsMain = false;
        }

        // Ensure at least one image is main
        var hasMainImage = imageDtos.Any(img => img.IsMain);
        if (!hasMainImage && imageDtos.Count > 0)
        {
            imageDtos[0].IsMain = true;
        }

        foreach (var imageDto in imageDtos)
        {
            if (imageDto.Id.HasValue)
            {
                // Update existing image
                var existingImage = product.Images.FirstOrDefault(i => i.Id == imageDto.Id.Value);
                if (existingImage != null)
                {
                    existingImage.ImageUrl = imageDto.ImageUrl;
                    existingImage.IsMain = imageDto.IsMain;
                    imageRepository.Update(existingImage);
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

                await imageRepository.InsertAsync(newImage);
            }
        }
    }
}