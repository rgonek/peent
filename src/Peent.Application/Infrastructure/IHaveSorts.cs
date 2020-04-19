using System.Collections.Generic;
using Peent.Application.Common;

namespace Peent.Application.Infrastructure
{
    public interface IHaveSorts
    {
        IList<SortDto> Sort { get; }
    }
}
