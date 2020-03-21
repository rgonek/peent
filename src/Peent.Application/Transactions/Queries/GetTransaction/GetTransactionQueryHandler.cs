using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Application.Transactions.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, TransactionModel>
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public GetTransactionQueryHandler(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<TransactionModel> Handle(GetTransactionQuery query, CancellationToken token)
        {
            var transaction = await _db.Transactions
                .Include(x => x.Category)
                .Include(x => x.Entries)
                .Include(x => x.TransactionTags)
                .Include("TransactionTags.Tag")
                .SingleOrDefaultAsync(x => x.Id == query.Id,
                    cancellationToken: token);

            if (transaction == null)
                throw NotFoundException.Create<Tag>(x => x.Id, query.Id);

            return new TransactionModel(transaction);
        }
    }
}
