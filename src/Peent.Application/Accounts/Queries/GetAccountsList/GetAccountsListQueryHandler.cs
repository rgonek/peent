using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common;
using Peent.Application.Accounts.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Common;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, PagedResult<AccountModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetAccountsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<PagedResult<AccountModel>> Handle(GetAccountsListQuery query, CancellationToken token)
        {
            dynamic accountsQuery = _db.Accounts
                .Include(x => x.Currency)
                .Where(x => x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                            x.DeletionDate.HasValue == false)
                .OrderBy(x => x.CreationDate);

            if (query.Sort.Any())
                accountsQuery = Sort(accountsQuery, query.Sort);

            if (query.Filters.Any())
                accountsQuery = Filter(accountsQuery, query.Filters);

            var accountsPaged = await ((IQueryable<Account>)accountsQuery)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new AccountModel(x),
                    token);

            return accountsPaged;
        }

        private IOrderedQueryable<Account> Sort(IOrderedQueryable<Account> accountsQuery, IList<SortInfo> sortInfo)
        {
            for (var i = 0; i < sortInfo.Count; i++)
            {
                var sort = sortInfo[i];
                accountsQuery = sort.Field.FirstUp() switch
                {
                    nameof(Account.Name) => accountsQuery.SortBy(x => x.Name, sort.Direction, i),
                    nameof(Account.Description) => accountsQuery.SortBy(x => x.Description, sort.Direction, i),
                    nameof(Account.Type) => accountsQuery.SortBy(x => x.Type, sort.Direction, i),
                    "Currency.name" => accountsQuery.SortBy(x => x.Currency.Name, sort.Direction, i),
                    _ => accountsQuery
                };
            }

            return accountsQuery;
        }

        private IQueryable<Account> Filter(IQueryable<Account> accountsQuery, IList<FilterInfo> filters)
        {
            foreach (var filter in filters.Where(x => x.Values.Any(y => y.HasValue())))
            {
                accountsQuery = filter.Field.FirstUp() switch
                {
                    nameof(Account.Name) => accountsQuery
                        .Where(x => x.Name.Contains(filter.Values.FirstOrDefault())),
                    nameof(Account.Description) => accountsQuery
                        .Where(x => x.Description.Contains(filter.Values.FirstOrDefault())),
                    nameof(Account.Type) => accountsQuery
                        .Where(x => filter.Values.Select(y => (AccountType)int.Parse(y)).Contains(x.Type)),
                    FilterInfo.Global => accountsQuery
                        .Where(x => x.Name.Contains(filter.Values.FirstOrDefault()) ||
                                                x.Description.Contains(filter.Values.FirstOrDefault())),
                    _ => accountsQuery
                };
            }

            return accountsQuery;
        }
    }
}
