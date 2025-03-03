using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
	public void Configure(EntityTypeBuilder<ProductType> builder)
	{
		builder.HasKey(p => p.Id);

		builder.HasOne(p => p.Product)
		   .WithMany(p => p.ProductTypes)
		   .HasForeignKey(p => p.ProductId)
		   .OnDelete(DeleteBehavior.Restrict); 

		builder.HasMany(p => p.ProductTypeDetails)
			.WithOne(p => p.ProductType)
			.HasForeignKey(p => p.ProductTypeId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
