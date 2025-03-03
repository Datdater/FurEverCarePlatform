using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductTypeDetail : BaseEntity
{
	[StringLength(255)]
	public required string Name { get; set; }
    public Guid? ProductTypeId { get; set; }

	//navigation
    public virtual ProductType ProductType { get; set; } 
}
