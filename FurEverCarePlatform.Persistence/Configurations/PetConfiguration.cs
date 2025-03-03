using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.PetType)
                .IsRequired(false);

            builder.Property(p => p.Age)
                .IsRequired(false);

            builder.Property(p => p.SpecialRequirement)
                .HasMaxLength(500);

            builder.Property(p => p.Color)
                .HasMaxLength(50);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasOne(p => p.AppUser)
                .WithMany(u => u.Pets)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Breed)
                .WithMany(b => b.Pets)
                .HasForeignKey(p => p.BreedId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.HealthDetails)
                .WithOne(h => h.Pet)
                .HasForeignKey(h => h.PetId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.BookingDetails)
                .WithOne(b => b.Pet)
                .HasForeignKey(b => b.PetId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Pet");
        }
    }
}