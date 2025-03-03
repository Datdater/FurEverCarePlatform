using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookingTime)
                .IsRequired();

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.Property(b => b.RawAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.Distance)
                .IsRequired();

            builder.HasOne(b => b.AppUser)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Promotion)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PromotionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Feedback)
                .WithOne(f => f.Booking)
                .HasForeignKey<Booking>(b => b.FeedbackId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Delivery)
                .WithMany(d => d.Bookings)
                .HasForeignKey(b => b.DeliveryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
