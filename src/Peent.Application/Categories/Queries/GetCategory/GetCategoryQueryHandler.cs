using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Categories.Models;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryModel>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetCategoryQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<CategoryModel> Handle(GetCategoryQuery query, CancellationToken token)
        {
            var category = await _db.Categories
                .SingleOrDefaultAsync(x => x.Id == query.Id &&
                    x.Workspace.Id == _userAccessor.User.GetWorkspaceId(),
                    cancellationToken: token);

            if (category == null)
                throw NotFoundException.Create<Category>(x => x.Id, query.Id);

            return new CategoryModel(category);
        }
    }
}
