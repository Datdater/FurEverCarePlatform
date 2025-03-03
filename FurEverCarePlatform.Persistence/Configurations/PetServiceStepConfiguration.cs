using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;
namespace FurEverCarePlatform.Persistence.Configurations
{
    public class PetServiceStepConfiguration : IEntityTypeConfiguration<PetServiceStep>
    {
        public void Configure(EntityTypeBuilder<PetServiceStep> builder)
        {
            builder.ToTable("PetServiceStep");

            builder.HasKey(pss => pss.Id);

            builder.Property(pss => pss.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(pss => pss.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(pss => pss.Priority)
                .IsRequired();

            builder.HasOne(pss => pss.PetService)
                .WithMany(ps => ps.PetServiceSteps)
                .HasForeignKey(pss => pss.PetServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pss => pss.ServiceTracking)
                .WithOne(st => st.PetServiceStep)
                .HasForeignKey<ServiceTracking>(st => st.PetServiceStepId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}