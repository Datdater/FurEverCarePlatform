using System.ComponentModel.DataAnnotations;
namespace FurEverCarePlatform.Domain.Entities;

public class ServiceTracking : BaseEntity
{
    [MaxLength(500)]
    public string Link { get; set; }
    public bool Status { get; set; }
    public Guid BookingDetailServiceId { get; set; }
    public Guid PetServiceStepId { get; set; }

    public virtual BookingDetailService BookingDetailService { get; set; }
    public virtual PetServiceStep PetServiceStep { get; set; }
}
