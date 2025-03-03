using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FurEverCarePlatform.Domain.Entities;
namespace FurEverCarePlatform.Persistence.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(os => os.Id);

            builder.Property(os => os.Description)
                .HasMaxLength(255);

            builder.Property(os => os.Status)
                .IsRequired();

            builder.HasOne(os => os.Order)
                .WithMany(o => o.OrderStatus)
                .HasForeignKey(os => os.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}