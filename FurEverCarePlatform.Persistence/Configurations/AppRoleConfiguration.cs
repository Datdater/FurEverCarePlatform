using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(r => r.NormalizedName)
                .IsUnique()
                .HasDatabaseName("RoleNameIndex");

            builder.Property(r => r.NormalizedName)
                .HasMaxLength(256);
        }
    }
}
