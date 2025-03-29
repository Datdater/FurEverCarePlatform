using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        RuleFor(p => p.ProductTypes)
               .Must(BeUniqueProductTypeNames)
               .WithMessage("Product type names must be unique.");

        RuleFor(p => p.ProductPrices)
            .Must(HaveDifferentProductTypeDetails)
            .WithMessage("Product type details (ProductTypeDetails1 and ProductTypeDetails2) must be different when both are provided.");
    }

    private bool BeUniqueProductTypeNames(List<ProductTypeDTO> productTypes)
    {
        if (productTypes == null || !productTypes.Any())
            return true;

        var duplicateNames = productTypes
            .GroupBy(pt => pt.Name?.Trim(), StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        return duplicateNames.Count == 0;
    }

    private bool HaveDifferentProductTypeDetails(List<ProductPricesDTO> productPrices)
    {
        if (productPrices == null || !productPrices.Any())
            return true;

        foreach (var price in productPrices)
        {
            if (!string.IsNullOrWhiteSpace(price.ProductTypeDetails1) &&
                !string.IsNullOrWhiteSpace(price.ProductTypeDetails2))
            {
                if (string.Equals(
                    price.ProductTypeDetails1.Trim(),
                    price.ProductTypeDetails2.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
