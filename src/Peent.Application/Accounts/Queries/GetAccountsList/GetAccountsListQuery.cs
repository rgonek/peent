using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Accounts.Models;
using Peent.Application.Common.DynamicQuery.Contracts;
using Peent.Application.Currencies.Models;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQuery : IRequest<PagedResult<AccountModel>>,
        IHavePagination, IHaveFilters, IHaveSorts, IHaveAllowedFields
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortDto> Sorts { get; } = new List<SortDto>();
        public IList<FilterDto> Filters { get; } = new List<FilterDto>();

        public IEnumerable<string> AllowedFields => new[]
        {
            nameof(AccountModel.Name),
            nameof(AccountModel.Description),
            nameof(AccountModel.Type),
            nameof(AccountModel.Currency) + "." + nameof(CurrencyModel.Name)
        };
    }
}
