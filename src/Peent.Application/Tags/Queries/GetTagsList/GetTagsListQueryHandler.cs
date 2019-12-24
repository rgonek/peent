using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Tags.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Common;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, PagedResult<TagModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTagsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<PagedResult<TagModel>> Handle(GetTagsListQuery query, CancellationToken token)
        {
            dynamic tagsQuery = _db.Tags
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                            x.DeletionDate.HasValue == false)
                .OrderBy(x => x.CreationDate);

            if (query.Sort.Any())
                tagsQuery = Sort(tagsQuery, query.Sort);

            if (query.Filters.Any())
                tagsQuery = Filter(tagsQuery, query.Filters);

            var tagsPaged = await ((IQueryable<Tag>)tagsQuery)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TagModel(x),
                    token);

            return tagsPaged;
        }

        private IOrderedQueryable<Tag> Sort(IOrderedQueryable<Tag> tagsQuery, IList<SortInfo> sortInfo)
        {
            for (var i = 0; i < sortInfo.Count; i++)
            {
                var sort = sortInfo[i];
                tagsQuery = sort.Field.FirstUp() switch
                {
                    nameof(Tag.Name) => tagsQuery.SortBy(x => x.Name, sort.Direction, i),
                    nameof(Tag.Description) => tagsQuery.SortBy(x => x.Description, sort.Direction, i),
                    nameof(Tag.Date) => tagsQuery.SortBy(x => x.Date, sort.Direction, i),
                    _ => tagsQuery
                };
            }

            return tagsQuery;
        }

        private IQueryable<Tag> Filter(IQueryable<Tag> tagsQuery, IList<FilterInfo> filters)
        {
            foreach (var filter in filters.Where(x => x.Values.Any(y => y.HasValue())))
            {
                tagsQuery = filter.Field.FirstUp() switch
                {
                    nameof(Tag.Name) => tagsQuery.Where(x => x.Name.Contains(filter.Values.FirstOrDefault())),
                    nameof(Tag.Description) => tagsQuery.Where(x =>
                        x.Description.Contains(filter.Values.FirstOrDefault())),
                    FilterInfo.Global => tagsQuery.Where(x => x.Name.Contains(filter.Values.FirstOrDefault()) ||
                                                x.Description.Contains(filter.Values.FirstOrDefault())),
                    _ => tagsQuery
                };
            }

            return tagsQuery;
        }
    }
}
