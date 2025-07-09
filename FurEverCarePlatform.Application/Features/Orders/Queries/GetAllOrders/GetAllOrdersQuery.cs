using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Models.Orders;
using FurEverCarePlatform.Domain.Enums;

namespace FurEverCarePlatform.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<Pagination<GetAllOrdersResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public EnumOrderStatus? Status { get; set; }
    }
}
