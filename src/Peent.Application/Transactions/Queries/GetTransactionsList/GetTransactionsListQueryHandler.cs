using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.DynamicQuery;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Specifications;
using Peent.Application.Transactions.Models;
using Peent.Application.Transactions.Specifications;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, PagedResult<TransactionModel>>
    {
        private readonly IApplicationDbContext _db;

        public GetTransactionsListQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<PagedResult<TransactionModel>> Handle(GetTransactionsListQuery query, CancellationToken token)
            => await _db.Transactions
                .Specify(new TransactionDetails())
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TransactionModel(x),
                    token);
    }
}
