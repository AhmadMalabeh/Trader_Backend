using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("Ads");

            builder.HasKey(a => a.ID);

            builder.Property(a => a.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(3000);

            builder.Property(a => a.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(a => a.Location)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.ContactPhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(a => a.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(a => a.MainImageURL)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(a => a.UserID)
                .IsRequired();

            builder.Property(a => a.CategoryID)
                .IsRequired();


            builder.HasOne(a => a.Category)
                .WithMany(c => c.Ads)
                .HasForeignKey(a => a.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.AdImages)
                .WithOne(ai => ai.Ad)
                .HasForeignKey(ai => ai.AdID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.FavoritedByUsers)
                .WithOne(ufa => ufa.Ad)
                .HasForeignKey(ufa => ufa.AdID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(a => a.AdData, b =>
            {
                b.ToJson();
            });



        }
    }
}
