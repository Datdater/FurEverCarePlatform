using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Persistence.Configurations
{
    

    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Min)
                .IsRequired();

            builder.Property(d => d.Max)
                .IsRequired();

            builder.Property(d => d.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.HasMany(d => d.Bookings)
                .WithOne(b => b.Delivery)
                .HasForeignKey(b => b.DeliveryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
