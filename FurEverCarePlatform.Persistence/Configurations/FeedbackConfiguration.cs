using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Persistence.Configurations
{

    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Detail)
                .HasMaxLength(1000);

            builder.Property(f => f.Rating)
                .IsRequired(false);

            builder.Property(f => f.Attachment)
                .HasMaxLength(500);

            builder.Property(f => f.Status)
                .IsRequired();

            builder.HasOne(f => f.AppUser)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }

}
