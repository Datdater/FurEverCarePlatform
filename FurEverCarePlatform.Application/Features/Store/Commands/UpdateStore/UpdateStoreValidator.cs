namespace FurEverCarePlatform.Application.Features.Store.Commands.UpdateStore;

public class UpdateStoreValidator : AbstractValidator<UpdateStoreCommand>
{
    public UpdateStoreValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
    }
}
