using System.Text.Json;

namespace FurEverCarePlatform.Domain.Entities;

public class OrderDetail : BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public JsonDocument Attribute { get; set; }
    public string ProductVariationId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Guid? FeedbackId { get; set; }

	public virtual Feedback Feedback { get; set; }

    public virtual Product Product { get; set; }    

}
