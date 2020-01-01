using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<PagedResult<TagModel>>
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortInfo> Sort { get; set; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; set; } = new List<FilterInfo>();
    }
}
