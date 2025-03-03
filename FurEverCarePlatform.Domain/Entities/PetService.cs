namespace FurEverCarePlatform.Domain.Entities;

public class PetService : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid StoreId { get; set; }
    public DateTime EstimatedTime { get; set; }
    public Guid ServiceCategoryId { get; set; }
    public Guid CategoryServiceId { get; set; }
    
    //navigation
    public virtual Store Store { get; set; }
    public virtual ServiceCategory ServiceCategory  { get; set; }
    public virtual ICollection<PetServiceStep> PetServiceSteps { get; set; }
    public virtual ICollection<PetServiceDetail> PetServiceDetails { get; set; }
    public virtual ICollection<ComboService> ComboServices { get; set; } 
}
