using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
	public void Configure(EntityTypeBuilder<Promotion> builder)
	{
		builder.HasKey(p => p.Id);
		builder.HasOne(p => p.Store)
			.WithMany(p => p.Promotions)
			.HasForeignKey(p => p.StoreId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(p => p.Bookings)
			.WithOne(p => p.Promotion)
			.HasForeignKey(p => p.PromotionId)
			.OnDelete(DeleteBehavior.Restrict); 

		builder.HasMany(p => p.Orders)
			.WithOne(p => p.Promotion)
			.HasForeignKey(p => p.PromotionId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
