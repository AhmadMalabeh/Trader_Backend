using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class UserFavoriteAdConfiguration : IEntityTypeConfiguration<UserFavoriteAd>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteAd> builder)
        {
            builder.ToTable("UserFavoriteAds");

            builder.HasKey(ufa => ufa.ID);

            builder.Property(ufa => ufa.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(ufa => ufa.UserID)
                .IsRequired();

            builder.Property(ufa => ufa.AdID)
                .IsRequired();

            builder.Property(ufa => ufa.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
