using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateCategoryCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<int> Handle(CreateCategoryCommand command, CancellationToken token)
        {
            var existingCategory = await _db.Categories
                .SingleOrDefaultAsync(x =>
                    x.Name == command.Name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (existingCategory != null)
                throw DuplicateException.Create<Category>(x => x.Name, command.Name);

            var category = new Category(
                command.Name,
                command.Description,
                _userAccessor.User.GetWorkspaceId());

            _db.Categories.Add(category);

            await _db.SaveChangesAsync(token);

            return category.Id;
        }
    }
}
