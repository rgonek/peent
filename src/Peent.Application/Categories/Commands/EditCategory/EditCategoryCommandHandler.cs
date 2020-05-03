using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;

namespace Peent.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditCategoryCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(EditCategoryCommand command, CancellationToken token)
        {
            var category = await _db.Categories.GetAsync(command.Id, token);
            category.SetName(command.Name);
            category.SetDescription(command.Description);

            _db.Attach(category);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}