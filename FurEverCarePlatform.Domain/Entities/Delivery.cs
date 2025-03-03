using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public float Min { get; set; }
        public float Max { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
