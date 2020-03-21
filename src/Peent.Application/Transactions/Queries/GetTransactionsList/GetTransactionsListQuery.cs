using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQuery : IRequest<PagedResult<TransactionModel>>
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortInfo> Sort { get; set; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; set; } = new List<FilterInfo>();
    }
}
