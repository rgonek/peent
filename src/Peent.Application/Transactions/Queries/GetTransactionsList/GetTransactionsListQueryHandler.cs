using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common;
using Peent.Application.Tags.Models;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Application.Transactions.Models;
using Peent.Common;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, PagedResult<TransactionModel>>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTransactionsListQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<PagedResult<TransactionModel>> Handle(GetTransactionsListQuery query, CancellationToken token)
        {
            dynamic transactionsQuery = _db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Entries)
                .Include("Entries.Account")
                .Include("Entries.Account.Currency")
                .Include(x => x.TransactionTags)
                .Include("TransactionTags.Tag")
                .OrderBy(x => x.CreationDate);

            if (query.Sort.Any())
                transactionsQuery = Sort(transactionsQuery, query.Sort);

            if (query.Filters.Any())
                transactionsQuery = Filter(transactionsQuery, query.Filters);

            var transactionsPaged = await ((IQueryable<Transaction>)transactionsQuery)
                .GetPagedAsync(
                    query.PageIndex,
                    query.PageSize,
                    x => new TransactionModel(x),
                    token);

            return transactionsPaged;
        }

        private IOrderedQueryable<Transaction> Sort(IOrderedQueryable<Transaction> transactionsQuery, IList<SortDto> sorts)
        {
            for (var i = 0; i < sorts.Count; i++)
            {
                var sort = sorts[i];
                transactionsQuery = sort.Field.FirstUp() switch
                {
                    nameof(Transaction.Title) => transactionsQuery.SortBy(x => x.Title, sort.Direction, i),
                    nameof(Transaction.Category) => transactionsQuery.SortBy(x => x.Category.Name, sort.Direction, i),
                    nameof(Transaction.Description) => transactionsQuery.SortBy(x => x.Description, sort.Direction, i),
                    nameof(Transaction.Date) => transactionsQuery.SortBy(x => x.Date, sort.Direction, i),
                    nameof(Transaction.Type) => transactionsQuery.SortBy(x => x.Type, sort.Direction, i),
                    _ => transactionsQuery
                };
            }

            return transactionsQuery;
        }

        private IQueryable<Transaction> Filter(IQueryable<Transaction> transactionsQuery, IList<FilterDto> filters)
        {
            foreach (var filter in filters.Where(x => x.Values.Any(y => y.HasValue())))
            {
                transactionsQuery = filter.Field.FirstUp() switch
                {
                    nameof(Transaction.Title) => transactionsQuery.Where(x => x.Title.Contains(filter.Values.FirstOrDefault())),
                    nameof(Transaction.Category) => transactionsQuery.Where(x => x.Category.Name.Contains(filter.Values.FirstOrDefault())),
                    nameof(Transaction.Description) => transactionsQuery.Where(x => x.Description.Contains(filter.Values.FirstOrDefault())),
                    //nameof(Transaction.Date) => transactionsQuery.Where(x => x.Description.Contains(filter.Values.FirstOrDefault())),
                    nameof(Transaction.Type) => transactionsQuery.Where(x => x.Type.ToString().ToLower() == filter.Values.FirstOrDefault()),
                    FilterDto.Global => transactionsQuery.Where(x => x.Title.Contains(filter.Values.FirstOrDefault()) ||
                                                                      x.Description.Contains(filter.Values.FirstOrDefault()) ||
                                                                      x.Category.Name.Contains(filter.Values.FirstOrDefault()) ||
                                                                      x.Type.ToString().ToLower() == filter.Values.FirstOrDefault()),
                    _ => transactionsQuery
                };
            }

            return transactionsQuery;
        }
    }
}
