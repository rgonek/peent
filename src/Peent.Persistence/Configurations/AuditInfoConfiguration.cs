using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Common;
using Peent.Domain.ValueObjects;

namespace Peent.Persistence.Configurations
{
    public static class AuditInfoConfiguration
    {
        public static void ConfigureAuditInfo<T>(this EntityTypeBuilder<T> builder)
            where T : class, IHaveAuditInfo
        {
            builder.Property(y => y.CreationDate)
                    .HasColumnName(nameof(CreationInfo.CreationDate))
                    .IsRequired();
            builder.Property(y => y.CreatedById)
                    .HasColumnName(nameof(CreationInfo.CreatedById))
                    .IsRequired();
            builder.HasOne(y => y.CreatedBy)
                    .WithMany()
                    .HasForeignKey(y => y.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(y => y.LastModificationDate)
                .HasColumnName(nameof(ModificationInfo.LastModificationDate));
            builder.Property(y => y.LastModifiedById)
                .HasColumnName(nameof(ModificationInfo.LastModifiedById));
            builder.HasOne(y => y.LastModifiedBy)
                .WithMany()
                .HasForeignKey(y => y.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
