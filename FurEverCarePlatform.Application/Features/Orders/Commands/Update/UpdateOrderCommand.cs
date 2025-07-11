using FurEverCarePlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Update
{
    public  class UpdateOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public EnumOrderStatus EnumOrderStatus { get; set; }
    }
}
