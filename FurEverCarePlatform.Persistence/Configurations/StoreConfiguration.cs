using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Persistence.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
	public void Configure(EntityTypeBuilder<Store> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(255);

		builder.Property(p => p.Hotline)
			.IsRequired()
			.HasMaxLength(20);

		builder.Property(p => p.LogoUrl)
			.HasMaxLength(500);

		builder.Property(p => p.BannerUrl)
			.HasMaxLength(500);

		builder.Property(p => p.BusinessType)
			.HasMaxLength(255);

		builder.Property(p => p.BusinessAddressProvince)
			.HasMaxLength(255);

		builder.Property(p => p.BusinessAddressDistrict)
			.HasMaxLength(255);

		builder.Property(p => p.BusinessAddressWard)
			.HasMaxLength(255);

		builder.Property(p => p.BusinessAddressStreet)
			.HasMaxLength(255);

		builder.Property(p => p.FaxEmail)
			.HasMaxLength(255);

		builder.Property(p => p.FaxCode)
			.HasMaxLength(50);

		builder.Property(p => p.FrontIdentityCardUrl)
			.HasMaxLength(500);

		builder.Property(p => p.BackIdentityCardUrl)
			.HasMaxLength(500);

		builder.Property(p => p.UserId)
			.IsRequired();

		builder.HasOne(p => p.Address)
			.WithOne(a => a.Store)
			.HasForeignKey<Store>(p => p.AddressId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(p => p.Promotions)
			.WithOne(p => p.Store)
			.HasForeignKey(p => p.StoreId)
			.OnDelete(DeleteBehavior.Restrict); 

		builder.HasMany(p => p.Products)
			.WithOne(p => p.Store)
			.HasForeignKey(p => p.StoreId)
			.OnDelete(DeleteBehavior.Restrict); 

		builder.HasMany(p => p.PetServices)
			.WithOne(p => p.Store)
			.HasForeignKey(p => p.StoreId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}