using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public CreateCategoryCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<int> Handle(CreateCategoryCommand command, CancellationToken token)
        {
            var category = new Category(
                command.Name,
                command.Description);

            _db.Categories.Attach(category);
            await _db.SaveChangesAsync(token);

            return category.Id;
        }
    }
}