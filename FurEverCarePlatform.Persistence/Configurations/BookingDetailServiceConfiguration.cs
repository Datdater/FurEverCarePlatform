using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class BookingDetailServiceConfiguration : IEntityTypeConfiguration<BookingDetailService>
    {
        public void Configure(EntityTypeBuilder<BookingDetailService> builder)
        {
            builder.HasKey(bds => bds.Id);

            builder.Property(bds => bds.BookingDetailId)
                .IsRequired();

            builder.Property(bds => bds.PetServiceDetailId)
                .IsRequired();

            builder.HasOne(bds => bds.BookingDetail)
                .WithMany(bd => bd.BookingDetailServices)
                .HasForeignKey(bds => bds.BookingDetailId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bds => bds.PetServiceDetail)
                .WithMany(psd => psd.BookingDetailServices)
                .HasForeignKey(bds => bds.PetServiceDetailId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
