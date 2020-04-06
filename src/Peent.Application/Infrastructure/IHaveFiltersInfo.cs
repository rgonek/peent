using System.Collections.Generic;
using Peent.Application.Common;

namespace Peent.Application.Infrastructure
{
    public interface IHaveFiltersInfo
    {
        IList<FilterInfo> Filters { get; }
    }
}
