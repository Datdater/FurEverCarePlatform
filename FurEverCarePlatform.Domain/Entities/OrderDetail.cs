using System.Text.Json;

namespace FurEverCarePlatform.Domain.Entities;

public class OrderDetail : BaseEntity
{
    public Guid ProductVariationId { get; set; }
    public string ProductionName { get; set; }
    public string ProductVariationName { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }

    public Guid? FeedbackId { get; set; }
    public Guid OrderId { get; set; }

    public virtual ProductVariant ProductVariation { get; set; }
    public virtual Order Order { get; set; }

    public virtual Feedback Feedback { get; set; }

    public virtual Product Product { get; set; }
}
