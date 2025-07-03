namespace Payment.API.DTO
{
    public class PaymentCreatedResponse
    {
        public Guid Id { get; set; }
        public string? PaymentUrl { get; set; }
    }
}
