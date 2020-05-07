using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Common.DynamicQuery;
using Peent.Application.Common.Extensions;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, PagedResult<TagModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _currentContextService;

        public GetTagsListQueryHandler(IApplicationDbContext db, ICurrentContextService currentContextService)
            => (_db, _currentContextService) = (db, currentContextService);

        public async Task<PagedResult<TagModel>> Handle(GetTagsListQuery query, CancellationToken token)
            => await _db.Tags
                .Where(x => x.Workspace.Id == _currentContextService.Workspace.Id)
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TagModel(x),
                    token);
    }
}
