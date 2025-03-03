namespace FurEverCarePlatform.Domain.Entities;

public class OrderDetail : BaseEntity
{
	public required Guid OrderId { get; set; }

	public required Guid ProductId { get; set; }

	public int Quantity { get; set; }

	public virtual Product Product { get; set; }

	public virtual Order Order { get; set; }

	public Guid? FeedbackId { get; set; }

	public virtual Feedback Feedback { get; set; }

}
