using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;
[Table("ServiceCategories")]
public class ServiceCategory : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<PetService> PetServices { get; set; }
}
