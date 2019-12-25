using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Accounts.Models;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQuery : IRequest<PagedResult<AccountModel>>
    {
        public int PageSize { get; set; } = 1;

        public int PageIndex { get; set; } = 10;

        public IList<SortInfo> Sort { get; set; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; set; } = new List<FilterInfo>();
    }
}
