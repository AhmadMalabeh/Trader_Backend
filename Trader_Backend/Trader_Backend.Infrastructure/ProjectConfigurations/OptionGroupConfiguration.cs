using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class OptionGroupConfiguration : IEntityTypeConfiguration<OptionGroup>
    {
        public void Configure(EntityTypeBuilder<OptionGroup> builder)
        {
            builder.ToTable("OptionGroups");

            builder.HasKey(og => og.ID);

            builder.Property(og => og.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(og => og.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(og => og.Value)
                .IsUnique()
                .HasDatabaseName("UX_OptionGroups_Value");

            builder.HasMany(og => og.Options)
                .WithOne(o => o.OptionGroup)
                .HasForeignKey(o => o.OptionGroupID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
