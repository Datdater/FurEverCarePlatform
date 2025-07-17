using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Domain.Entities
{
    public class ProductReviews : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid AppUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? OrderDetailId { get; set; } // Thêm OrderId nếu cần liên kết với đơn hàng
        public OrderDetail? OrderDetail { get; set; } // Navigation property nếu dùng EF Core

        // Navigation property nếu dùng EF Core
        public virtual Product Product { get; set; }
        public virtual AppUser AppUser { get; set; }

        public ProductReviews()
            : base() { }
    }
}
