using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(cm => cm.Id);

            builder.Property(cm => cm.UserId)
                .IsRequired();

            builder.Property(cm => cm.Content)
                .HasMaxLength(255);

            builder.Property(cm => cm.IsRead)
                .IsRequired();

            builder.Property(cm => cm.ToUserId)
                .IsRequired();

            builder.Property(cm => cm.Type)
                .HasMaxLength(50);

            builder.Property(cm => cm.IsSend)
                .IsRequired();

            builder.Property(cm => cm.Reaction)
                .HasMaxLength(50);

            builder.Property(cm => cm.IsUserDeleted)
                .IsRequired();

            builder.Property(cm => cm.IdToUserDeleted)
                .IsRequired();

            builder.Property(cm => cm.AttachmentName)
                .HasMaxLength(255);

            builder.Property(cm => cm.FieldAttachmentUrl)
                .HasMaxLength(500);

            builder.HasOne(cm => cm.AppUser)
                .WithMany(u => u.ChatMessage)
                .HasForeignKey(cm => cm.UserId);

            builder.HasOne(cm => cm.ToAppUser)
                .WithMany()
                .HasForeignKey(cm => cm.ToUserId);
        }
    }
}
