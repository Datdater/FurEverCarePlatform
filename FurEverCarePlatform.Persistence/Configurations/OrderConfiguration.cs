using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;
namespace FurEverCarePlatform.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalPrice)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(o => o.Distance)
                .IsRequired();

            builder.HasOne(o => o.AppUser)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Promotion)
                .WithMany()
                .HasForeignKey(o => o.PromotionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}