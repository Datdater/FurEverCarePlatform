using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasKey(p => p.Id);

		builder.HasOne(p => p.ProductCategory)
			.WithMany(pc => pc.Products)
			.HasForeignKey(p => p.ProductCategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.ProductBrand)
			.WithMany(pb => pb.Products)
			.HasForeignKey(p => p.BrandId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.Store)
			.WithMany(s => s.Products)
			.HasForeignKey(p => p.StoreId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(p => p.OrderDetails)
			.WithOne(od => od.Product)
			.HasForeignKey(od => od.ProductId)
			.OnDelete(DeleteBehavior.NoAction); 

		builder.HasMany(p => p.ProductTypes)
			.WithOne(pt => pt.Product)
			.HasForeignKey(pt => pt.ProductId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
