using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class PetServiceConfiguration : IEntityTypeConfiguration<PetService>
    {
        public void Configure(EntityTypeBuilder<PetService> builder)
        {
            builder.ToTable("PetService");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ps => ps.Description)
                .HasMaxLength(1000);

            builder.HasOne(ps => ps.Store)
                .WithMany(s => s.PetServices)
                .HasForeignKey(ps => ps.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ps => ps.ServiceCategory)
                .WithMany(sc => sc.PetServices)
                .HasForeignKey(ps => ps.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ps => ps.PetServiceSteps)
                .WithOne(pst => pst.PetService)
                .HasForeignKey(pst => pst.PetServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(ps => ps.PetServiceDetails)
                .WithOne(psd => psd.PetService)
                .HasForeignKey(psd => psd.PetServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(ps => ps.ComboServices)
                .WithOne(cs => cs.PetService)
                .HasForeignKey(cs => cs.PetServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}