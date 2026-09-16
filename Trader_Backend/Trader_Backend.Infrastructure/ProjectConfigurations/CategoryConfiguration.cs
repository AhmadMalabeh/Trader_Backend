using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.ID);

            builder.Property(c => c.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.HasIndex(c => c.Name)
                .IsUnique()
                .HasDatabaseName("UX_Categories_Name");

            builder.HasMany(c => c.OptionGroups)
                .WithOne(og => og.Category)
                .HasForeignKey(og => og.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
