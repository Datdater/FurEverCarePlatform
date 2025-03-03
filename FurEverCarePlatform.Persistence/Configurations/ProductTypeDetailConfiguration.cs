using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ProductTypeDetailConfiguration : IEntityTypeConfiguration<ProductTypeDetail>
{
	public void Configure(EntityTypeBuilder<ProductTypeDetail> builder)
	{
		builder.HasKey(p => p.Id);
		builder.HasOne(p => p.ProductType)
			.WithMany(p => p.ProductTypeDetails)
			.HasForeignKey(p => p.ProductTypeId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
