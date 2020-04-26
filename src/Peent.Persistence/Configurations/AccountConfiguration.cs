using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Currency)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ConfigureWorkspace();
            builder.ConfigureAuditInfo();
        }
    }
}
