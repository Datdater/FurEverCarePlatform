namespace FurEverCarePlatform.Domain.Entities;

public class BookingDetailService : BaseEntity
{
    public Guid BookingDetailId { get; set; }
    public Guid PetServiceDetailId { get; set; }

    //navigation
    public virtual BookingDetail BookingDetail { get; set; }
    public virtual PetServiceDetail PetServiceDetail { get; set; }

    public virtual ICollection<ServiceTracking> ServiceTrackings { get; set; }
}
