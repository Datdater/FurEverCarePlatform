using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
	public class CreatePetServiceValidator : AbstractValidator<CreatePetServiceCommand>
	{
		public CreatePetServiceValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull()
				.MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

			RuleFor(p => p.Description)
				.MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

			RuleFor(p => p.StoreId)
				.NotEmpty().WithMessage("{PropertyName} is required.");

			RuleFor(p => p.EstimatedTime)
				.GreaterThan(DateTime.MinValue).WithMessage("EstimatedTime must be a valid date.");

			RuleFor(p => p.ServiceCategoryId)
				.NotEmpty().WithMessage("{PropertyName} is required.");

			RuleForEach(p => p.PetServiceDetails).SetValidator(new CreatePetServiceDetailValidator());
		}

		public class CreatePetServiceDetailValidator : AbstractValidator<CreatePetServiceDetailCommand>
		{
			public CreatePetServiceDetailValidator()
			{
				RuleFor(p => p.PetWeightMin)
					.GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be at least 0.");

				RuleFor(p => p.PetWeightMax)
					.GreaterThan(p => p.PetWeightMin).WithMessage("PetWeightMax must be greater than PetWeightMin.");

				RuleFor(p => p.Amount)
					.GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

				RuleFor(p => p.Name)
					.NotEmpty().WithMessage("{PropertyName} is required.")
					.MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

				RuleFor(p => p.Description)
					.MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
			}
		}

	}

}
