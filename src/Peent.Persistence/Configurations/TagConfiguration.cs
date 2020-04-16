﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Workspace)
                .WithMany()
                .HasForeignKey(x => x.WorkspaceId)
                .IsRequired();

            builder.HasMany(x => x.TransactionTags)
                .WithOne()
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata.FindNavigation(nameof(Tag.TransactionTags))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.ConfigureAuditInfo();
        }
    }
}
