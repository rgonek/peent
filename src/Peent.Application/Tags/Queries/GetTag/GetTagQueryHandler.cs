using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, TagModel>
    {
        private readonly IApplicationDbContext _db;

        public GetTagQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<TagModel> Handle(GetTagQuery query, CancellationToken token)
        {
            var tag = await _db.Tags.FindAsync(new[] {query.Id}, token);

            return new TagModel(tag);
        }
    }
}
