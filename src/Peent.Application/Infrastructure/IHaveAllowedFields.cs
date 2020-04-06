using System.Collections.Generic;

namespace Peent.Application.Infrastructure
{
    public interface IHaveAllowedFields
    {
        IEnumerable<string> AllowedFields { get; }
    }
}
