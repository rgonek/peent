using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Categories.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IList<CategoryModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetCategoriesListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<IList<CategoryModel>> Handle(GetCategoriesListQuery query, CancellationToken token)
        {
            var categories = await _db.Categories
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionDate.HasValue == false)
                .ToListAsync(token);

            return categories.Select(x => new CategoryModel(x)).ToList();
        }
    }
}
