using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductPrice : BaseEntity
{
	public Guid ProductTypeDetails1 { get; set; } 
	public Guid? ProductTypeDetails2 { get; set; } 
	[Column(TypeName = "money")]
	public required decimal Price { get; set; } 
	public required int Inventory { get; set; } 

	//navigation
	[ForeignKey("ProductTypeDetails1")]
	public virtual ProductTypeDetail ProductType1 { get; set; }

	[ForeignKey("ProductTypeDetails2")]
	public virtual ProductTypeDetail ProductType2 { get; set; }
}
