using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Infrastructure;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQuery : IRequest<PagedResult<TransactionModel>>,
        IHavePagination, IHaveFilters, IHaveSorts
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortDto> Sorts { get; } = new List<SortDto>();
        public IList<FilterDto> Filters { get; } = new List<FilterDto>();
    }
}
