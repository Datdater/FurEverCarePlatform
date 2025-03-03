using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class BookingDetailConfiguration : IEntityTypeConfiguration<BookingDetail>
    {
        public void Configure(EntityTypeBuilder<BookingDetail> builder)
        {
            builder.HasKey(bd => bd.Id);

            builder.Property(bd => bd.BookingServiceId)
                .IsRequired();

            builder.Property(bd => bd.PetId)
                .IsRequired();

            builder.Property(bd => bd.BookingTime)
                .IsRequired();

            builder.Property(bd => bd.RealAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(bd => bd.RawAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(bd => bd.PetWeight)
                .IsRequired();

            builder.Property(bd => bd.Hair)
                .HasMaxLength(100);

            builder.HasOne(bd => bd.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(bd => bd.BookingServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bd => bd.Pet)
                .WithMany(p => p.BookingDetails)
                .HasForeignKey(bd => bd.PetId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bd => bd.AssignedUser)
                .WithMany()
                .HasForeignKey(bd => bd.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(bd => bd.Combo)
                .WithMany(c => c.BookingDetails)
                .HasForeignKey(bd => bd.ComboId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
