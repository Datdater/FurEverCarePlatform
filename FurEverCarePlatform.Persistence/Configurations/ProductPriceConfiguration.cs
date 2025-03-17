using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
	public void Configure(EntityTypeBuilder<ProductPrice> builder)
	{
		builder.HasKey(p => p.Id);

		builder.HasOne(p => p.ProductType1)
			.WithMany()
			.HasForeignKey(p => p.ProductTypeDetails1)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.ProductType2)
			.WithMany()
			.HasForeignKey(p => p.ProductTypeDetails2)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
