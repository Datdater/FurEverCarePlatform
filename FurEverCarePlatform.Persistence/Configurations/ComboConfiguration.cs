using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class ComboConfiguration : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.DiscountPercent)
                .IsRequired();

            builder.HasMany(c => c.ComboServices)
                .WithOne(cs => cs.Combo)
                .HasForeignKey(cs => cs.ComboId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.BookingDetails)
                .WithOne(bd => bd.Combo)
                .HasForeignKey(bd => bd.ComboId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}