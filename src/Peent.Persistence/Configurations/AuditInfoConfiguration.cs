using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Persistence.Configurations
{
    public static class AuditInfoConfiguration
    {
        public static void ConfigureAuditInfo<T>(this EntityTypeBuilder<T> builder)
            where T : class, IHaveAuditInfo
        {
            builder.OwnsOne(x => x.CreationInfo, x =>
            {
                x.Property(y => y.CreationDate)
                    .HasColumnName(nameof(CreationInfo.CreationDate))
                    .IsRequired();
                x.Property(y => y.CreatedById)
                    .HasColumnName(nameof(CreationInfo.CreatedById))
                    .IsRequired();
                x.HasOne(y => y.CreatedBy)
                    .WithMany()
                    .HasForeignKey(y => y.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.OwnsOne(x => x.ModificationInfo, x =>
            {
                x.Property(y => y.LastModificationDate)
                    .HasColumnName(nameof(ModificationInfo.LastModificationDate));
                x.Property(y => y.LastModifiedById)
                    .HasColumnName(nameof(ModificationInfo.LastModifiedById));
                x.HasOne(y => y.LastModifiedBy)
                    .WithMany()
                    .HasForeignKey(y => y.LastModifiedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.OwnsOne(x => x.DeletionInfo, x =>
            {
                x.Property(y => y.DeletionDate)
                    .HasColumnName(nameof(DeletionInfo.DeletionDate));
                x.Property(y => y.DeletedById)
                    .HasColumnName(nameof(DeletionInfo.DeletedById));
                x.HasOne(y => y.DeletedBy)
                    .WithMany()
                    .HasForeignKey(y => y.DeletedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
