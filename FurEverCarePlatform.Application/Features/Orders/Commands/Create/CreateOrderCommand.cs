using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FurEverCarePlatform.Domain.Enums;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommand : IRequest
    {
        public Guid StoreId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PromotionId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string Note { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}

public class OrderDetailDTO
{
    public int Quantity { get; set; }
    public Guid ProductVariationId { get; set; }
}
