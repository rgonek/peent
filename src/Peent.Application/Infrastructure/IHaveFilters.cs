using System.Collections.Generic;
using Peent.Application.Common;

namespace Peent.Application.Infrastructure
{
    public interface IHaveFilters
    {
        IList<FilterDto> Filters { get; }
    }
}
