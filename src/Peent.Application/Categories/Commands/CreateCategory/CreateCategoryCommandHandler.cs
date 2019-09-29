using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description,
                WorkspaceId = _userAccessor.User.GetWorkspaceId()
            };
            category.SetCreatedBy(_userAccessor.User.GetUserId());

            _db.Categories.Add(category);

            await _db.SaveChangesAsync(token);

            return category.Id;
        }
    }
}
