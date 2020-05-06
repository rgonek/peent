using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public CreateTagCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<int> Handle(CreateTagCommand command, CancellationToken token)
        {
            var tag = new Tag(
                command.Name,
                command.Description);

            _db.Tags.Attach(tag);

            await _db.SaveChangesAsync(token);

            return tag.Id;
        }
    }
}
