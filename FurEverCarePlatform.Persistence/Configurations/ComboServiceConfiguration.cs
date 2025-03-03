using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class ComboServiceConfiguration : IEntityTypeConfiguration<ComboService>
    {
        public void Configure(EntityTypeBuilder<ComboService> builder)
        {
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.ComboId)
                .IsRequired();

            builder.Property(cs => cs.PetServiceId)
                .IsRequired();

            builder.HasOne(cs => cs.Combo)
                .WithMany(c => c.ComboServices)
                .HasForeignKey(cs => cs.ComboId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(cs => cs.PetService)
                .WithMany(ps => ps.ComboServices)
                .HasForeignKey(cs => cs.PetServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
