﻿using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurEverCarePlatform.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(au => au.Id);

            builder.Property(au => au.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(au => au.CreationDate);
            builder.Property(au => au.CreatedBy);
            builder.Property(au => au.ModificationDate);
            builder.Property(au => au.ModifiedBy);
            builder.Property(au => au.DeleteDate);
            builder.Property(au => au.DeletedBy);
            builder.Property(au => au.IsDeleted)
                .IsRequired();

            builder.HasOne(au => au.Store)
                .WithOne(s => s.AppUser)
                .HasForeignKey<Store>(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(au => au.Orders)
                .WithOne(o => o.AppUser)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(au => au.Bookings)
                .WithOne(b => b.AppUser)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(au => au.Pets)
                .WithOne(p => p.AppUser)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(au => au.Address)
                .WithOne(a => a.AppUser)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(au => au.Notifications)
                .WithOne(n => n.AppUser)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(au => au.Feedback)
                .WithOne(f => f.AppUser)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(au => au.ChatMessage)
                .WithOne(cm => cm.AppUser)
                .HasForeignKey(cm => cm.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(au => au.Reports)
                .WithOne(r => r.AppUser)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
