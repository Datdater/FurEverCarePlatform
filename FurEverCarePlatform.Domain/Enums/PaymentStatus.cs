namespace FurEverCarePlatform.Domain.Enums;

public enum PaymentStatus
{
    Pending = 0,      // Giao dịch vừa được tạo, đang chờ thanh toán
    Completed = 1,    // Thanh toán thành công (xác nhận từ cổng thanh toán hoặc thủ công)
    Failed = 2,       // Thanh toán thất bại (quá hạn, sai số tiền, lỗi cổng thanh toán)
    Cancelled = 3,    // Giao dịch bị hủy (do Order hủy trước khi thanh toán)
    Refunded = 4      // Giao dịch được hoàn tiền (sau khi thanh toán, nếu Order hủy)
}
