using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
using Peent.Domain.ValueObjects;

namespace Peent.Persistence.Configurations
{
    public class TransactionEntryConfiguration : IEntityTypeConfiguration<TransactionEntry>
    {
        public void Configure(EntityTypeBuilder<TransactionEntry> builder)
        {
            builder.ToTable("TransactionEntries").HasKey(x => x.Id);

            builder.HasOne(x => x.Transaction)
                .WithMany(x => x.Entries)
                .IsRequired();

            builder.HasOne(x => x.Account)
                .WithMany()
                .IsRequired();

            builder.OwnsOne(x => x.Money, b =>
            {
                const string currencyIdColumnName = nameof(Money.Currency) + nameof(Currency.Id);
                b.Property<int>(currencyIdColumnName).HasColumnName(currencyIdColumnName);
                b.HasOne(x => x.Currency)
                    .WithMany()
                    .HasForeignKey(currencyIdColumnName)
                    .IsRequired();
                b.Property(x => x.Amount)
                    .HasColumnName(nameof(Money.Amount))
                    .HasColumnType("decimal(38,18)")
                    .IsRequired();
            });

//            builder.HasOne(x => x.Currency)
//                .WithMany()
//                .IsRequired();
//
//            builder.Property(x => x.Amount)
//                .HasColumnType("decimal(38,18)")
//                .IsRequired();

            builder.ConfigureAuditInfo();
        }
    }
}