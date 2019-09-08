﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public CreateAccountCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<int> Handle(CreateAccountCommand command, CancellationToken token)
        {
            var account = new Account
            {
                Name = command.Name,
                Description = command.Description,
                Type = command.Type,
                CurrencyId = command.CurrencyId,
                CreationInfo = new CreationInfo(_userAccessor.User.GetUserId()),
                WorkspaceId = _userAccessor.User.GetWorkspaceId()
            };

            _db.Accounts.Add(account);

            await _db.SaveChangesAsync(token);

            return account.Id;
        }
    }
}
