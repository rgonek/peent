using Microsoft.EntityFrameworkCore;
using Peent.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peent.Persistence.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(3)
                .IsRequired();
            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Symbol)
                .HasMaxLength(12)
                .IsRequired();
            builder.Property(x => x.DecimalPlaces)
                .IsRequired();
        }
    }
}
