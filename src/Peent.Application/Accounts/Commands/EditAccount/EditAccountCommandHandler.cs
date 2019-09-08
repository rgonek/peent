﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using Peent.Domain.ValueObjects;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public EditAccountCommandHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(EditAccountCommand command, CancellationToken token)
        {
            var account = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id &&
                        x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    token);

            if (account == null)
                throw NotFoundException.Create<Account>(x => x.Id, command.Id);

            var existingAccount = await _db.Accounts
                .SingleOrDefaultAsync(x =>
                    x.Id != command.Id &&
                    x.Name == command.Name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionInfo.DeletionDate.HasValue == false,
                    token);

            if (existingAccount != null)
                throw DuplicateException.Create<Account>(x => x.Name, command.Name);

            account.Name = command.Name;
            account.Description = command.Description;
            account.Type = command.Type;
            account.CurrencyId = command.CurrencyId;
            account.ModificationInfo = new ModificationInfo(_userAccessor.User.GetUserId());

            _db.Update(account);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}