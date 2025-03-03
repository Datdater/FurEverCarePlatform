using System.ComponentModel.DataAnnotations;
namespace FurEverCarePlatform.Domain.Entities;

public class ServiceCategory : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<PetService> PetServices { get; set; }
}
