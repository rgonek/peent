using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;

namespace Peent.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryModel>
    {
        private readonly IApplicationDbContext _db;

        public GetCategoryQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<CategoryModel> Handle(GetCategoryQuery query, CancellationToken token)
        {
            var category = await _db.Categories.GetAsync(query.Id, token);

            return new CategoryModel(category);
        }
    }
}