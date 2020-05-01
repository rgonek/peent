using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public DeleteCategoryCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken token)
        {
            var category = await _db.Categories
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.Workspace.Id == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (category == null)
                throw NotFoundException.Create<Category>(x => x.Id, command.Id);

            _db.Remove(category);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
