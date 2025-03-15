using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking
{
	class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CreateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
		{
			await _unitOfWork.BeginTransactionAsync();
			var validator = new CreateBookingValidator();
			var validationResult = await validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw new BadRequestException(validationResult.ToString(), validationResult);
			}
			try
			{
				var booking = _mapper.Map<Domain.Entities.Booking>(request);
				booking.BookingDetails = CreateBookingDetail(request);
				await _unitOfWork.GetRepository<Domain.Entities.Booking>().InsertAsync(booking);
				await _unitOfWork.SaveAsync();
				await _unitOfWork.CommitTransactionAsync();
				return booking.Id;
			}
			catch (System.Exception ex)
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw new BadRequestException($"Booking failed: {ex.Message} | InnerException: {ex.InnerException?.Message}", ex);
			}
		}

		private ICollection<Domain.Entities.BookingDetail> CreateBookingDetail(CreateBookingCommand request)
		{
			var bookingDetails = new List<Domain.Entities.BookingDetail>();
			foreach (var bd in request.BookingDetails)
			{
				var bookingDetail = new Domain.Entities.BookingDetail
				{
					Pet = _mapper.Map<Pet>(bd.Pet),
					RealAmount = CalculateTotalAmount(bd),
					BookingTime = request.BookingTime,
					ComboId = bd.Combo?.Id,
					BookingDetailServices = bd.Services
						.Select(service => new BookingDetailService
						{
							PetServiceDetailId = service.Id 
						})
						.ToList()
				};
				bookingDetails.Add(bookingDetail);
			}
			return bookingDetails;
		}

		private decimal CalculateTotalAmount(BookingDetailCommand bookingDetails)
		{
			decimal totalAmount = 0;
			foreach (var bd in bookingDetails.Services)
			{
				totalAmount += bd.Amount;
			}
			return totalAmount;
		}

	}
}
