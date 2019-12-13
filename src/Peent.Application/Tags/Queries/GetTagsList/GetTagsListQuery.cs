using System.Collections.Generic;
using System.Net;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<PagedResult<TagModel>>
    {
        public int PageSize { get; set; } = 1;

        public int PageIndex { get; set; } = 10;

        public IList<SortInfo> Sort { get; set; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; set; } = new List<FilterInfo>();
    }

    public class FilterInfo
    {
        public string Field { get; set; }
        public IList<string> Values { get; set; } = new List<string>();

        public static readonly string Global = "_";
    }
}
