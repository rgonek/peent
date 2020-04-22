using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common;
using Peent.Application.Accounts.Models;
using Peent.Application.DynamicQuery;
using Peent.Application.Infrastructure.Extensions;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, PagedResult<AccountModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetAccountsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public async Task<PagedResult<AccountModel>> Handle(GetAccountsListQuery query, CancellationToken token)
            => await _db.Accounts
                .Include(x => x.Currency)
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId())
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new AccountModel(x),
                    token);
    }
}
