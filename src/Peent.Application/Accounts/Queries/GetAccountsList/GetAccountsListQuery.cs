using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Accounts.Models;
using Peent.Application.Currencies.Models;
using Peent.Application.Infrastructure;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQuery : IRequest<PagedResult<AccountModel>>,
        IHavePaginationInfo, IHaveFiltersInfo, IHaveSortsInfo, IHaveAllowedFields
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortInfo> Sort { get; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; } = new List<FilterInfo>();

        public IEnumerable<string> AllowedFields => new[]
        {
            nameof(AccountModel.Name),
            nameof(AccountModel.Description),
            nameof(AccountModel.Type),
            nameof(AccountModel.Currency) + "." + nameof(CurrencyModel.Name)
        };
    }
}
