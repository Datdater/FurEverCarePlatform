using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;

public class ComboService : BaseEntity
{
	public Guid ComboId { get; set; }
	public Guid PetServiceId { get; set; }

	//navigation
	public virtual Combo Combo { get; set; }
	public virtual PetService PetService { get; set; }
}
