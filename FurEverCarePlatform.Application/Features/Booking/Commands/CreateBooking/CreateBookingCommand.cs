using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking
{
     public class CreateBookingCommand : IRequest<Guid>
    {
        public DateTime BookingTime { get; set; }
        public string? Description { get; set; }
        public required Guid UserId { get; set; }
        public Guid? PromotionId { get; set; }

        public Guid StoreId { get; set; }

        public required ICollection<BookingDetailCommand> BookingDetails { get; set; }
    }

    public class BookingDetailCommand
    {
        public required PetCommand Pet { get; set; }
        public required ICollection<ServiceDetailCommand> Services { get; set; }
    }

    public class PetCommand
    {
        public required Guid Id { get; set; }
    }

    public class ServiceDetailCommand
    {
        public required Guid Id { get; set; }
    }
}
