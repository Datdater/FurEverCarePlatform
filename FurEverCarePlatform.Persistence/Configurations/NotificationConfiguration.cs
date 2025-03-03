using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;
namespace FurEverCarePlatform.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(n => n.ReturnUrl)
                .HasMaxLength(500);

            builder.Property(n => n.IsRead)
                .IsRequired();

            builder.HasOne(n => n.AppUser)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(n => n.FromAppUserId)
                .WithMany()
                .HasForeignKey(n => n.FromUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}