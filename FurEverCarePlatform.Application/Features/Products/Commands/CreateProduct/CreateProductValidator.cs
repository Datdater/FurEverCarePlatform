using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FurEverCarePlatform.Application.Features.Products.DTOs;

namespace FurEverCarePlatform.Application.Features.Products.Commands.CreateProduct;
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.BasePrice)
            .GreaterThan(0)
            .WithMessage("Base price must be greater than 0");

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage("Weight must be greater than 0");

        RuleFor(x => x.Length)
            .GreaterThan(0)
            .WithMessage("Length must be greater than 0");

        RuleFor(x => x.Height)
            .GreaterThan(0)
            .WithMessage("Height must be greater than 0");

        // Validate variants
        RuleForEach(x => x.Variants)
            .SetValidator(new ProductVariantValidator())
            .When(x => x.Variants?.Any() == true);

        // Validate images
        RuleForEach(x => x.Images)
            .SetValidator(new ProductImageValidator())
            .When(x => x.Images?.Any() == true);

        // Ensure only one main image
        RuleFor(x => x.Images)
            .Must(images => images == null || !images.Any() || images.Count(img => img.IsMain) <= 1)
            .WithMessage("Only one image can be marked as main");
    }
}

public class ProductVariantValidator : AbstractValidator<CreateProductVariantDTO>
{
    public ProductVariantValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Variant price must be greater than 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock cannot be negative");

        RuleFor(x => x.Attributes)
            .NotNull()
            .WithMessage("Variant attributes are required")
            .Must(attributes => attributes.Any())
            .WithMessage("At least one attribute is required for the variant");
    }
}

public class ProductImageValidator : AbstractValidator<CreateProductImageDTO>
{
    public ProductImageValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithMessage("Image URL is required")
            .Must(BeAValidUrl)
            .WithMessage("Image URL must be a valid URL");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result) &&
               (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}