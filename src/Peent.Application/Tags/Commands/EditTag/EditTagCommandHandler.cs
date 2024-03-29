﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandHandler : IRequestHandler<EditTagCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditTagCommandHandler(IApplicationDbContext db, ICurrentContextService userAccessor)
            => _db = db;

        public async Task<Unit> Handle(EditTagCommand command, CancellationToken token)
        {
            var tag = await _db.Tags.GetAsync(command.Id, token);
            tag.SetName(command.Name);
            tag.SetDescription(command.Description);

            _db.Attach(tag);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
