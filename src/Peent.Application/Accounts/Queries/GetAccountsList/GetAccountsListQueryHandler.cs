using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Accounts.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, IList<AccountModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetAccountsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<IList<AccountModel>> Handle(GetAccountsListQuery query, CancellationToken token)
        {
            var accounts = await _db.Accounts
                .Include(x => x.Currency)
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                            x.DeletionInfo.DeletionDate.HasValue == false)
                .ToListAsync(token);

            return accounts.Select(x => new AccountModel(x)).ToList();
        }
    }
}
