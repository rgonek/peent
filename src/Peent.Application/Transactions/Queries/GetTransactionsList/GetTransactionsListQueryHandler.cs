using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Interfaces;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, IList<TransactionModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTransactionsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<IList<TransactionModel>> Handle(GetTransactionsListQuery query, CancellationToken token)
        {
            var dbQuery = _db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Entries)
                .Include("Entries.Account")
                .Include("Entries.Account.Currency")
                .Skip(query.PageInfo.SkipRecords)
                .Take(query.PageInfo.PageSize);

            if (query.Categories.Any())
            {
                dbQuery.Where(x => query.Categories.Contains(x.CategoryId));
            }

            if (query.Accounts.Any())
            {
                dbQuery.Where(x => x.Entries.Any(y => query.Accounts.Contains(y.AccountId)));
            }

            if (query.From.HasValue)
            {
                dbQuery.Where(x => x.Date >= query.From.Value);
            }

            if (query.To.HasValue)
            {
                dbQuery.Where(x => x.Date <= query.To.Value);
            }

            var transactions = await dbQuery.ToListAsync(token);

            return transactions.Select(x => new TransactionModel(x)).ToList();
        }
    }
}
