using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(2000);
            builder.Property(x => x.Date)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            builder.HasMany(x => x.Entries)
                .WithOne()
                .HasForeignKey(x => x.TransactionId)
                .IsRequired();

            builder.HasMany(x => x.TransactionTags)
                .WithOne()
                .HasForeignKey(x => x.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ConfigureAuditInfo();
        }
    }
}
