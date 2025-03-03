using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
	public void Configure(EntityTypeBuilder<ServiceCategory> builder)
	{
		builder.HasKey(p => p.Id);
		builder.HasMany(p => p.PetServices)
		   .WithOne(p => p.ServiceCategory)
		   .HasForeignKey(p => p.ServiceCategoryId)
		   .OnDelete(DeleteBehavior.Restrict);
	}
}
