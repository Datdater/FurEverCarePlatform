namespace FurEverCarePlatform.Application.Models.Payments
{
    public class PaymentCreatedResponse
    {
        public Guid Id { get; set; }
        public string? PaymentUrl { get; set; }
    }
}
