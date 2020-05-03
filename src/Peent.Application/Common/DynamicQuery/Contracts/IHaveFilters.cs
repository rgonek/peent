using System.Collections.Generic;

namespace Peent.Application.Common.DynamicQuery.Contracts
{
    public interface IHaveFilters
    {
        IList<FilterDto> Filters { get; }
    }
}
