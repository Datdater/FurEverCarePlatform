using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.CoordinateX);

            builder.Property(a => a.CoordinateY);

            builder.Property(a => a.Province)
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode);

            builder.Property(a => a.UserId)
                .IsRequired();

            builder.HasOne(a => a.AppUser)
                .WithMany(u => u.Address)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Store)
                .WithOne(s => s.Address)
                .HasForeignKey<Store>(s => s.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Orders)
                .WithOne(o => o.Address)
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Bookings)
                .WithOne(b => b.Address)
                .HasForeignKey(b => b.UserAddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
