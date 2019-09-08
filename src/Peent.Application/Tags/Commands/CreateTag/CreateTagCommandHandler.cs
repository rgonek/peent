﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateTagCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<int> Handle(CreateTagCommand command, CancellationToken token)
        {
            var tag = new Tag
            {
                Name = command.Name,
                Description = command.Description,
                Date = command.Date,
                CreationInfo = new CreationInfo(_userAccessor.User.GetUserId()),
                WorkspaceId = _userAccessor.User.GetWorkspaceId()
            };

            _db.Tags.Add(tag);

            await _db.SaveChangesAsync(token);

            return tag.Id;
        }
    }
}
