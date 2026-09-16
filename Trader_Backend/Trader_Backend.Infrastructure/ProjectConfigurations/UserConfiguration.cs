using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure the User entity here
            builder.ToTable("Users");

            builder.HasKey(u => u.ID);

            builder.Property(u => u.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(u => u.EntraID)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.PersonalPictureURL)
                .HasMaxLength(300);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("User");

            builder.Property(u => u.RegisteredAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(u => u.EntraID)
                .IsUnique()
                .HasDatabaseName("UX_Users_EntraID");

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("UX_Users_Email");

            builder.HasMany(u => u.CreatedInvitationCodes)
                .WithOne(ui => ui.CreatedByAdmin)
                .HasForeignKey(ui => ui.CreatedByAdminID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.FavoriteAds)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Ads)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
