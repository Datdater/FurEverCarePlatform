using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;
public class ServiceTrackingConfiguration : IEntityTypeConfiguration<ServiceTracking>
{
    public void Configure(EntityTypeBuilder<ServiceTracking> builder)
    {

        builder.HasKey(st => st.Id);

        builder.Property(st => st.Link)
            .HasMaxLength(500);

        builder.Property(st => st.Status)
            .IsRequired();

        //builder.HasOne(st => st.BookingDetailService)
        //    .WithMany()
        //    .HasForeignKey(st => st.BookingDetailServiceId)
        //    .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(st => st.PetServiceStep)
            .WithOne(pss => pss.ServiceTracking)
            .HasForeignKey<ServiceTracking>(st => st.PetServiceStepId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
