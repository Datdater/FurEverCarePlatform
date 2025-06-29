using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Domain.Enums
{
    public enum PaymentMethod
    {
        CashOnDelivery = 0, // Thanh toán khi nhận hàng
        BankTransfer = 1, // Chuyển khoản ngân hàng
        CreditCard = 2, // Thẻ tín dụng
        EWallet = 3, // Ví điện tử (Momo, ZaloPay, v.v.)
        PayPal = 4, // PayPal
    }
}
