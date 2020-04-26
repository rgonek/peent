using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Common;

namespace Peent.Persistence.Configurations
{
    public static class AuditInfoConfiguration
    {
        public static void ConfigureAuditInfo<T>(this EntityTypeBuilder<T> builder)
            where T : class, IHaveAuditInfo
        {

            builder.OwnsOne(x => x.Created).Property(x => x.On).IsRequired();
            builder.OwnsOne(x => x.LastModified);
        }
    }
}
