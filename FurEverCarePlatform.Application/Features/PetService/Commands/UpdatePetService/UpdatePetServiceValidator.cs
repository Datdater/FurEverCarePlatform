using FluentValidation;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService
{
    public class UpdatePetServiceValidator : AbstractValidator<UpdatePetServiceCommand>
	{
		public UpdatePetServiceValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

			RuleFor(p => p.Description)
				.MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

			RuleFor(p => p.StoreId)
				.NotEmpty().WithMessage("{PropertyName} is required.");

			RuleFor(p => p.EstimatedTime)
				.NotEmpty().WithMessage("EstimatedTime is required.")
				.Matches(@"^\d+\s*-\s*\d+\s*(minutes|hours)$")
				.WithMessage("EstimatedTime must be in the format 'X - Y minutes/hours'.");

			RuleFor(p => p.ServiceCategoryId)
				.NotEmpty().WithMessage("{PropertyName} is required.");

			RuleFor(p => p.Status)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.Must(status => status == true || status == false).WithMessage("{PropertyName} must be either true or false.");

			RuleForEach(p => p.PetServiceDetails)
				.SetValidator(new UpdatePetServiceDetailValidator());

			RuleForEach(p => p.PetServiceSteps)
				.SetValidator(new UpdatePetServiceStepValidator());

			RuleFor(p => p.PetServiceSteps)
				.Must(BeInAscendingOrder)
				.When(p => p.PetServiceSteps != null && p.PetServiceSteps.Any())
				.WithMessage("Priority must be in ascending order (e.g., 1, 2, 3...).");
		}

		private bool BeInAscendingOrder(List<UpdatePetServiceStepCommand> steps)
		{
			var priorities = steps.Select(s => s.Priority).ToList();
			return priorities.SequenceEqual(priorities.OrderBy(p => p));
		}
	}

	public class UpdatePetServiceDetailValidator : AbstractValidator<UpdatePetServiceDetailCommand>
	{
		public UpdatePetServiceDetailValidator()
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

	public class UpdatePetServiceStepValidator : AbstractValidator<UpdatePetServiceStepCommand>
	{
		public UpdatePetServiceStepValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

			RuleFor(p => p.Description)
				.MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

			RuleFor(p => p.Priority)
				.GreaterThanOrEqualTo(1)
				.WithMessage("Priority must be greater than or equal to 1.");
		}
	}

}
