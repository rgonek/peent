using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public class TransactionEntryConfiguration : IEntityTypeConfiguration<TransactionEntry>
    {
        public void Configure(EntityTypeBuilder<TransactionEntry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Transaction)
                .WithMany()
                .HasForeignKey(x => x.TransactionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .IsRequired();

            builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .IsRequired();

            builder.HasOne(x => x.ForeignCurrency)
                .WithMany()
                .HasForeignKey(x => x.ForeignCurrencyId)
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
