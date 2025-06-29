
namespace FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookingCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var bookingCode = Utils.UtilityHelper.GenerateRandomCode(6);

        var booking = new Domain.Entities.Booking
        {
            Id = Guid.NewGuid(),
            BookingTime = command.BookingTime,
            Description = command.Description,
            UserId = command.UserId,
            Code = bookingCode,
            RawAmount = 0, // Will be calculated
            TotalAmount = 0, // Will be calculated  
            PromotionId = command.PromotionId,
            BookingDetails = new List<BookingDetail>()
        };

        var bookingDetails = new List<BookingDetail>();

        foreach (var detailCommand in command.BookingDetails)
        {
            var pet = await _unitOfWork.GetRepository<Pet>().GetByIdAsync(detailCommand.Pet.Id);
            if (pet == null)
            {
                throw new NotFoundException("Pet", detailCommand.Pet.Id);
            }

            foreach (var serviceCommand in detailCommand.Services)
            {
                var service = await _unitOfWork.GetRepository<PetServiceDetail>().GetByIdAsync(serviceCommand.Id);
                if (service == null)
                {
                    throw new NotFoundException("Service", serviceCommand.Id);
                }

                var bookingDetail = new BookingDetail
                {
                    Id = Guid.NewGuid(),
                    ServiceId = serviceCommand.Id,
                    PetId = detailCommand.Pet.Id,
                    BookingTime = command.BookingTime,
                    RealAmount = 0, // Will be set during service execution
                    IsMeasured = false, // Will be updated when pet is measured
                    RawAmount = service.Amount, // Use service price as raw amount
                    PetWeight = null, // Will be measured later
                    Hair = null, // Will be updated during service
                    AssignedUserId = null, // Will be assigned later
                    Booking = booking
                };

                bookingDetails.Add(bookingDetail);
            }
        }

        booking.BookingDetails = bookingDetails;

        CalculateBookingTotals(booking);

        await _unitOfWork.GetRepository<Domain.Entities.Booking>().InsertAsync(booking);

        await _unitOfWork.SaveAsync();

        return booking.Id;
    }

    private void CalculateBookingTotals(Domain.Entities.Booking booking)
    {
        float rawAmount = 0;
        float totalAmount = 0;

        foreach (var detail in booking.BookingDetails)
        {
            var detailAmount = detail.RealAmount > 0 ? detail.RealAmount : detail.RawAmount;
            rawAmount += detailAmount;
        }

        booking.RawAmount = rawAmount;

        totalAmount = rawAmount;
        if (booking.PromotionId.HasValue)
        {
        }

        booking.TotalAmount = totalAmount;
    }
}