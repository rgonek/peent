using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common.Specifications;
using Peent.Application.Exceptions;
using Peent.Application.Transactions.Models;
using Peent.Application.Transactions.Specifications;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, TransactionModel>
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _userAccessor;

        public GetTransactionQueryHandler(IApplicationDbContext db, ICurrentContextService userAccessor)
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        public async Task<TransactionModel> Handle(GetTransactionQuery query, CancellationToken token)
        {
            var transaction = await _db.Transactions
                .Specify(new TransactionDetails())
                .SingleOrDefaultAsync(x => x.Id == query.Id,
                    cancellationToken: token);

            if (transaction == null)
                throw NotFoundException.Create<Tag>(x => x.Id, query.Id);

            return new TransactionModel(transaction);
        }
    }
}
