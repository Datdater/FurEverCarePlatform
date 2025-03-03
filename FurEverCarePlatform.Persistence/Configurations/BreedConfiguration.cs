using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.PetType)
                .IsRequired(false);

            builder.HasMany(b => b.Pets)
                .WithOne(p => p.Breed)
                .HasForeignKey(p => p.BreedId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
