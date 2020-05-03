using System.Collections.Generic;

namespace Peent.Application.Common.DynamicQuery.Contracts
{
    public interface IHaveAllowedFields
    {
        IEnumerable<string> AllowedFields { get; }
    }
}
