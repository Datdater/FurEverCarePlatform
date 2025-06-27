
namespace FurEverCarePlatform.Domain.Enums;
public enum EnumOrderStatus
{
    Pending = 0,          // Đơn hàng vừa được tạo, chưa xử lý
    PendingPayment = 1,   // Đang chờ thanh toán (dành cho BANK_TRANSFER)
    Confirmed = 2,        // Đơn hàng được xác nhận (thanh toán COD hoặc BANK_TRANSFER hoàn tất)
    Processing = 3,       // Đang xử lý (chuẩn bị hàng, đóng gói)
    Shipped = 4,          // Đã giao cho đơn vị vận chuyển
    Delivered = 5,        // Đã giao hàng thành công
    Cancelled = 6,        // Đơn hàng bị hủy
    PaymentFailed = 7,    // Thanh toán thất bại (dành cho BANK_TRANSFER)
    Returned = 8
}