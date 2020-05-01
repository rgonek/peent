using Peent.Domain.Entities;

namespace Peent.Domain.Common
{
    public interface IHaveWorkspace
    {
        Workspace Workspace { get; }
    }
}
