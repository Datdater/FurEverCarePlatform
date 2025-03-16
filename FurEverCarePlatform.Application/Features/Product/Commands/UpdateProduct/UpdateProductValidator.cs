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
        RuleFor(p => p.ProductCategoryId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(p => p.BrandId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(p => p.StoreId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(p => p.Views)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(p => p.ProductTypes)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}
