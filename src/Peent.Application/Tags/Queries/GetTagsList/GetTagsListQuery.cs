using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Infrastructure;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<PagedResult<TagModel>>,
        IHavePaginationInfo, IHaveFiltersInfo, IHaveSortsInfo
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortInfo> Sort { get; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; } = new List<FilterInfo>();
    }
}
