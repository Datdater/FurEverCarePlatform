using FurEverCarePlatform.Domain.Entities;

public class Breed : BaseEntity
{
    public required string Name { get; set; }

    public bool? PetType { get; set; }
    public ICollection<Pet> Pets { get; set; }
}
