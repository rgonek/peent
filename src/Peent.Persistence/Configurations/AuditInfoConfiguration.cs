using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public static class AuditInfoConfiguration
    {
        public static void ConfigureAuditInfo<T>(this EntityTypeBuilder<T> builder)
            where T : AuditInfoOwner
        {
            builder.Property(x => x.CreationDate)
                .IsRequired();
            builder.Property(x => x.CreatedById)
                .IsRequired();
            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(y => y.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.LastModificationDate);
            builder.Property(x => x.LastModifiedById);
            builder.HasOne(x => x.LastModifiedBy)
                .WithMany()
                .HasForeignKey(y => y.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.DeletionDate);
            builder.Property(x => x.DeletedById);
            builder.HasOne(x => x.DeletedBy)
                .WithMany()
                .HasForeignKey(y => y.DeletedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
