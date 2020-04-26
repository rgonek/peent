using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities.TransactionAggregate;

namespace Peent.Persistence.Configurations
{
    public class TransactionTagConfiguration : IEntityTypeConfiguration<TransactionTag>
    {
        public void Configure(EntityTypeBuilder<TransactionTag> builder)
        {
            builder.HasKey(x => new
            {
                x.TagId,
                x.TransactionId
            });

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.TransactionTags)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Transaction)
                .WithMany(x => x.TransactionTags)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
