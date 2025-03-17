namespace FurEverCarePlatform.Application.Features.Store.Commands.CreateStore;

public class CreateStoreValidator : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(20)
            .WithMessage("{PropertyName} must not exceed 100 characters.");
    }
}
