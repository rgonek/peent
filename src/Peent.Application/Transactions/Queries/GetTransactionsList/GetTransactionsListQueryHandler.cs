using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common;
using Peent.Application.DynamicQuery;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, PagedResult<TransactionModel>>
    {
        private readonly IApplicationDbContext _db;

        public GetTransactionsListQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<PagedResult<TransactionModel>> Handle(GetTransactionsListQuery query, CancellationToken token)
            => await _db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Entries)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.Currency)
                .Include(x => x.TransactionTags)
                .ThenInclude(x => x.Tag)
                .ApplyFilters(query)
                .ApplySort(query)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TransactionModel(x),
                    token);
    }
}
