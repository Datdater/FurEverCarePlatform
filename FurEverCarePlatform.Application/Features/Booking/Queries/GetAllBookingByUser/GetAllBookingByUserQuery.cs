using FurEverCarePlatform.Application.Features.Booking.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Queries.GetAllBookingByUser
{
	public class GetAllBookingByUserQuery : IRequest<Pagination<GetAllBookingDto>>
	{
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;

		public Guid? AppUserId { get; set; }
		public Guid? StoreId { get; set; }

	}
}
