namespace FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryValidator : AbstractValidator<CreateProductCategoryCommand>
    {
        public CreateProductCategoryValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
