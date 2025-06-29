using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class PetServiceStep : BaseEntity
{
	[Required]
	[StringLength(255)]
	public string Name { get; set; }
	[Required]
	[StringLength(255)]
	public string Description { get; set; }
	public int Priority { get; set; }
	public Guid PetServiceId { get; set; }
	//navigation
	public virtual PetService PetService { get; set; }
}