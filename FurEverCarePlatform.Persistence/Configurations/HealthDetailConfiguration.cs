using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class HealthDetailConfiguration : IEntityTypeConfiguration<HealthDetail>
    {
        public void Configure(EntityTypeBuilder<HealthDetail> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Weight)
                .IsRequired();

            builder.Property(h => h.PetId)
                .IsRequired();

            builder.Property(h => h.MeasureDate)
                .IsRequired();

            builder.HasOne(h => h.Pet)
                .WithMany(p => p.HealthDetails)
                .HasForeignKey(h => h.PetId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
