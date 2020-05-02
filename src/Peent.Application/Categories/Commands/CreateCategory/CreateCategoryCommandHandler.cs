using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateCategoryCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<int> Handle(CreateCategoryCommand command, CancellationToken token)
        {
            await ThrowsIfDuplicateAsync(command, token);

            var category = new Category(
                command.Name,
                command.Description,
                Workspace.FromId(_userAccessor.User.GetWorkspaceId()));

            _db.Categories.Attach(category);
            await _db.SaveChangesAsync(token);

            return category.Id;
        }

        private async Task ThrowsIfDuplicateAsync(CreateCategoryCommand command, CancellationToken token)
        {
            var existingCategory = await _db.Categories
                .SingleOrDefaultAsync(x =>
                        x.Name == command.Name &&
                        x.Workspace.Id == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (existingCategory != null)
            {
                throw DuplicateException.Create<Category>(x => x.Name, command.Name);
            }
        }
    }
}