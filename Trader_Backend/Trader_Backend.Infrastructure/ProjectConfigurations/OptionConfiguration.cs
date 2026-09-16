using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("Options");

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(o => o.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.OptionGroupID)
                .IsRequired();

            builder.HasIndex(o => o.Value)
                .IsUnique()
                .HasDatabaseName("UX_Options_Value");
        }
    
    }
}
