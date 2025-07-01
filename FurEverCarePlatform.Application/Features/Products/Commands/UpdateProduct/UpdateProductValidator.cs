using FluentValidation;
using FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;
using FurEverCarePlatform.Application.Features.Products.DTOs;

namespace FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required");

        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage("Store ID is required");

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
            .SetValidator(new UpdateProductVariantValidator())
            .When(x => x.Variants?.Any() == true);

        // Validate images
        RuleForEach(x => x.Images)
            .SetValidator(new UpdateProductImageValidator())
            .When(x => x.Images?.Any() == true);

        // Ensure only one main image
        RuleFor(x => x.Images)
            .Must(images => images == null || !images.Any() || images.Count(img => img.IsMain) <= 1)
            .WithMessage("Only one image can be marked as main");

        // Validate unique variant attributes combinations
        RuleFor(x => x.Variants)
            .Must(HaveUniqueVariantCombinations)
            .WithMessage("Product variants must have unique attribute combinations")
            .When(x => x.Variants?.Any() == true);
    }

    private bool HaveUniqueVariantCombinations(List<UpdateProductVariantDTO> variants)
    {
        if (variants == null || variants.Count <= 1)
            return true;

        var attributeCombinations = variants.Select(v =>
            string.Join("|", v.Attributes.OrderBy(a => a.Key).Select(a => $"{a.Key}:{a.Value}"))
        ).ToList();

        return attributeCombinations.Count == attributeCombinations.Distinct().Count();
    }
}

public class UpdateProductVariantValidator : AbstractValidator<UpdateProductVariantDTO>
{
    public UpdateProductVariantValidator()
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

public class UpdateProductImageValidator : AbstractValidator<UpdateProductImageDTO>
{
    public UpdateProductImageValidator()
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