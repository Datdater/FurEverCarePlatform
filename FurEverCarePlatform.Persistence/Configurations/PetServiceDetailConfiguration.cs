using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;
namespace FurEverCarePlatform.Persistence.Configurations
{
    public class PetServiceDetailConfiguration : IEntityTypeConfiguration<PetServiceDetail>
    {
        public void Configure(EntityTypeBuilder<PetServiceDetail> builder)
        {
            builder.ToTable("PetServiceDetail");

            builder.HasKey(psd => psd.Id);

            builder.Property(psd => psd.PetWeightMin)
                .IsRequired();

            builder.Property(psd => psd.PetWeightMax)
                .IsRequired();

            builder.Property(psd => psd.Amount)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(psd => psd.PetType)
                .IsRequired();

            builder.Property(psd => psd.Description)
                .HasMaxLength(500);

            builder.Property(psd => psd.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(psd => psd.PetService)
                .WithMany(ps => ps.PetServiceDetails)
                .HasForeignKey(psd => psd.PetServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(psd => psd.BookingDetailServices)
                .WithOne(bds => bds.PetServiceDetail)
                .HasForeignKey(bds => bds.PetServiceDetailId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}