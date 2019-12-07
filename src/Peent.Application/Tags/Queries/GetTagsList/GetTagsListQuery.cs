using MediatR;
using Peent.Application.Common;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<PagedResult<TagModel>>
    {
        public int PageSize { get; set; } = 1;

        public int PageIndex { get; set; } = 10;
    }
}
