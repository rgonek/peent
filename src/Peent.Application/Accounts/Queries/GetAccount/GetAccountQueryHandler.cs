using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Application.Accounts.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountModel>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetAccountQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<AccountModel> Handle(GetAccountQuery query, CancellationToken token)
        {
            var account = await _db.Accounts
                .Include(x => x.Currency)
                .SingleOrDefaultAsync(x => x.Id == query.Id &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId(),
                    cancellationToken: token);

            if (account == null)
                throw NotFoundException.Create<Account>(x => x.Id, query.Id);

            return new AccountModel(account);
        }
    }
}
