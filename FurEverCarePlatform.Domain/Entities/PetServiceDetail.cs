using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities;

public class PetServiceDetail : BaseEntity
{
    public Guid PetServiceId { get; set; }
    public float PetWeightMin { get; set; }
    public float PetWeightMax { get; set; }

    [DataType(DataType.Currency)]
    public float Amount { get; set; }
    public bool PetType { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }

    //navigation
    public virtual PetService PetService { get; set; }

}
