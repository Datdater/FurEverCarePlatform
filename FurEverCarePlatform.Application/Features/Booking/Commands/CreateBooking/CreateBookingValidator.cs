using FluentValidation;
using System;

namespace FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking
{
	public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
	{
		public CreateBookingValidator()
		{
			RuleFor(x => x.BookingTime)
				.GreaterThan(DateTime.UtcNow).WithMessage("Booking time must be in the future.");

			RuleFor(x => x.RawAmount)
				.GreaterThan(0).WithMessage("Raw amount must be greater than 0.");

			RuleFor(x => x.TotalAmount)
				.GreaterThan(0).WithMessage("Total amount must be greater than 0.")
				.GreaterThanOrEqualTo(x => x.RawAmount)
				.WithMessage("Total amount must be greater than or equal to raw amount.");

			RuleFor(x => x.Distance)
				.GreaterThanOrEqualTo(0).WithMessage("Distance must be non-negative.");

			RuleFor(x => x.BookingDetails)
				.NotEmpty().WithMessage("At least one booking detail must be included.");

			RuleForEach(x => x.BookingDetails).SetValidator(new BookingDetailValidator());
		}
	}

	public class BookingDetailValidator : AbstractValidator<BookingDetailCommand>
	{
		public BookingDetailValidator()
		{
			RuleFor(x => x.Pet)
				.NotNull().WithMessage("Each booking detail must contain a pet.")
				.SetValidator(new PetValidator());

			RuleFor(x => x.Services)
				.NotEmpty().WithMessage("Each booking detail must include at least one service.");

			RuleForEach(x => x.Services).SetValidator(new ServiceDetailValidator());

		}
	}

	public class PetValidator : AbstractValidator<PetCommand>
	{
		public PetValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Pet name is required.")
				.MaximumLength(100).WithMessage("Pet name must be at most 100 characters long.");

			RuleFor(x => x.Age)
				.LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Pet age must be in the past.");

			RuleFor(x => x.Color)
				.MaximumLength(50).WithMessage("Pet color must be at most 50 characters long.");
		}
	}

	public class ServiceDetailValidator : AbstractValidator<ServiceDetailCommand>
	{
		public ServiceDetailValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("Service ID is required.");
			RuleFor(x => x.Amount)
				.GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
		}
	}

}
