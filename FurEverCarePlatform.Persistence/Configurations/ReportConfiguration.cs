using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
	public void Configure(EntityTypeBuilder<Report> builder)
	{
		builder.HasKey(p => p.Id);
		builder.HasOne(p => p.AppUser)
			.WithMany(p => p.Reports)
			.HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

	}
}
