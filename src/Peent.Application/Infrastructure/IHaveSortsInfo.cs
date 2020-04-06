using System.Collections.Generic;
using Peent.Application.Common;

namespace Peent.Application.Infrastructure
{
    public interface IHaveSortsInfo
    {
        IList<SortInfo> Sort { get; }
    }
}
