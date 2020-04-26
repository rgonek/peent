using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Persistence.Configurations
{
    public class TransactionEntryConfiguration : IEntityTypeConfiguration<TransactionEntry>
    {
        public void Configure(EntityTypeBuilder<TransactionEntry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany()
                .IsRequired();

            builder.HasOne(x => x.Currency)
                .WithMany()
                .IsRequired();

            builder.HasOne(x => x.ForeignCurrency)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(38,18)")
                .IsRequired();
            builder.Property(x => x.ForeignAmount)
                .HasColumnType("decimal(38,18)");

            builder.ConfigureAuditInfo();
        }
    }
}
