using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FurEverCarePlatform.Domain.Enums;
using MediatR;
using Payment.API.DTO;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommand : IRequest<PaymentCreatedResponse>
    {
        private Guid CustomerId;
        public Guid AddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PromotionId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string Note { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();

        public Guid GetCustomerId()
        {
            return CustomerId;
        }

        public void SetCustomerId(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}

public class OrderDetailDTO
{
    public int Quantity { get; set; }
    public Guid ProductVariationId { get; set; }
}
