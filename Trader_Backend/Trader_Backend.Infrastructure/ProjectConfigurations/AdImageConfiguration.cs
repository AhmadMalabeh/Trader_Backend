using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class AdImageConfiguration : IEntityTypeConfiguration<AdImage>
    {
        public void Configure(EntityTypeBuilder<AdImage> builder)
        {
            builder.ToTable("AdImages");

            builder.HasKey(ai => ai.ID);

            builder.Property(ai => ai.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(ai => ai.ImageUrl)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ai => ai.AdID)
                .IsRequired();
        }
    
    }
}
