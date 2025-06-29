using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Commands.UpdateBooking;

public class UpdateBookingHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookingCommand>
{
    public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var booking = await unitOfWork.GetRepository<Domain.Entities.Booking>().GetByIdAsync(request.Id);
        if (booking is null)
            throw new System.Exception($"Not found with id: {request.Id}");
        booking.Status = request.BookingStatus;
        unitOfWork.GetRepository<Domain.Entities.Booking>().Update(booking);
        await unitOfWork.SaveAsync();
    }
}
