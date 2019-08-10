using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Workspace)
                .WithMany()
                .HasForeignKey(x => x.WorkspaceId)
                .IsRequired();

            builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ConfigureAuditInfo();
        }
    }
}
