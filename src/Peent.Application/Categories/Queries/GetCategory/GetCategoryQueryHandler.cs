using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Categories.Models;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryModel>
    {
        private readonly IApplicationDbContext _db;

        public GetCategoryQueryHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CategoryModel> Handle(GetCategoryQuery query, CancellationToken token)
        {
            var category = await _db.Categories.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken: token);

            if (category == null)
                throw new NotFoundException(nameof(Category), query.Id);

            return new CategoryModel(category);
        }
    }
}
