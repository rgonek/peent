using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditCategoryCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(EditCategoryCommand command, CancellationToken token)
        {
            var category = await _db.Categories
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (category == null)
                throw NotFoundException.Create<Category>(x => x.Id, command.Id);

            var existingCategory = await _db.Categories
                .SingleOrDefaultAsync(x =>
                    x.Id != command.Id &&
                    x.Name == command.Name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (existingCategory != null)
                throw DuplicateException.Create<Category>(x => x.Name, command.Name);

            category.Name = command.Name;
            category.Description = command.Description;

            _db.Update(category);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
