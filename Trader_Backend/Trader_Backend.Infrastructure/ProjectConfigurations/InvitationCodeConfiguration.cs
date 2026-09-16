using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Infrastructure.ProjectConfigurations
{
    public class InvitationCodeConfiguration : IEntityTypeConfiguration<InvitationCode>
    {
        public void Configure(EntityTypeBuilder<InvitationCode> builder)
        {
            // Configure the InvitationCode entity here
            builder.ToTable("InvitationCodes");

            builder.HasKey(ic => ic.ID);

            builder.Property(ic => ic.ID)
                .UseIdentityColumn(1, 1);

            builder.Property(ic => ic.CodeHash)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(ic => ic.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ic => ic.UsedAt)
                .IsRequired(false);

            builder.Property(ic => ic.IsUsed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(ic => ic.CreatedByAdminID)
                .IsRequired();

            builder.HasIndex(ic => ic.CodeHash)
                .IsUnique()
                .HasDatabaseName("UX_InvitationCodes_CodeHash");

        }
    }
}
