using System.Collections.Generic;

namespace Peent.Application.Common.DynamicQuery.Contracts
{
    public interface IHaveSorts
    {
        IList<SortDto> Sorts { get; }
    }
}
