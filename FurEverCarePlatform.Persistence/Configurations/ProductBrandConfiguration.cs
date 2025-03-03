using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
	public void Configure(EntityTypeBuilder<ProductBrand> builder)
	{
		builder.HasKey(p => p.Id);

		builder.HasMany(p => p.Products)
			.WithOne(p => p.ProductBrand)
			.HasForeignKey(p => p.BrandId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
