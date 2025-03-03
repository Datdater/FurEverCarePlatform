using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
	public void Configure(EntityTypeBuilder<ProductCategory> builder)
	{
		builder.HasKey(p => p.Id);

		builder.HasOne(p => p.ParentCategory)
			.WithMany(p => p.SubCategories)
			.HasForeignKey(p => p.ParentCategoryId)
			.OnDelete(DeleteBehavior.Restrict); 

		builder.HasMany(p => p.Products)
			.WithOne(p => p.ProductCategory)
			.HasForeignKey(p => p.ProductCategoryId)
			.OnDelete(DeleteBehavior.Restrict); 

		builder.HasMany(p => p.SubCategories)
			.WithOne(p => p.ParentCategory)
			.HasForeignKey(p => p.ParentCategoryId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
