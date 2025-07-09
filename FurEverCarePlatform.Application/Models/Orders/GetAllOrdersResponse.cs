using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Models.Orders
{
    public class GetAllOrdersResponse
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedTime { get; set; }
        public decimal DeliveryPrice { get; set; }
        public List<GetOrderDetail> OrderDetailDTOs { get; set; }
        public string OrderStatus { get; set; }
    }
}
