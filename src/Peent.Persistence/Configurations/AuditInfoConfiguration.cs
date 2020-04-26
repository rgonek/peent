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
            builder.OwnsOne(x => x.CreationInfo, b =>
            {
                b.Property(x => x.CreationDate)
                    .IsRequired()
                    .HasColumnName(nameof(CreationInfo.CreationDate));
                b.Property(x => x.CreatedById)
                    .IsRequired()
                    .HasColumnName(nameof(CreationInfo.CreatedById));
            });
            builder.OwnsOne(x => x.LastModificationInfo, b =>
            {
                b.Property(x => x.LastModificationDate).HasColumnName(nameof(ModificationInfo.LastModificationDate));
                b.Property(x => x.LastModifiedById).HasColumnName(nameof(ModificationInfo.LastModifiedById));
            });
            builder.OwnsOne(x => x.LastModificationInfo);
        }
    }
}
