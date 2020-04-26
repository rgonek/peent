using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Persistence.Configurations
{
    public static class WorkspaceContainerConfiguration
    {
        public static void ConfigureWorkspace<T>(this EntityTypeBuilder<T> builder)
            where T : class, IHaveWorkspace
        {
            builder.HasOne<Workspace>()
                .WithMany()
                .HasForeignKey(x => x.WorkspaceId);
        }
    }
}
