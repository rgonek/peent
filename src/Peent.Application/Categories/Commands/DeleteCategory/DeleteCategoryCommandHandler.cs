using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Peent.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public DeleteCategoryCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken token)
        {
            _db.Remove(await _db.Categories.FindAsync(new[] {command.Id}, token));
            await _db.SaveChangesAsync(token);
            return default;
        }
    }
}