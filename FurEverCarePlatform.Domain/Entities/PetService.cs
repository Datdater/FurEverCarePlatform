using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities;

public class PetService : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int TotalUsed { get; set; }

    public int TotalReviews { get; set; }
    public float RatingAverage { get; set; }
    public Guid StoreId { get; set; }
    [MaxLength(100)]
    public required string EstimatedTime { get; set; }

    public bool Status { get; set; }
    public Guid ServiceCategoryId { get; set; }
    
    //navigation
    public virtual Store Store { get; set; }
    public virtual ServiceCategory ServiceCategory  { get; set; }
    public virtual ICollection<PetServiceStep> PetServiceSteps { get; set; }
    public virtual ICollection<PetServiceDetail> PetServiceDetails { get; set; }
}
