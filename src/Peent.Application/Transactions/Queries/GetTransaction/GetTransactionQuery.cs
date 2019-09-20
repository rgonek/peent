using MediatR;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionQuery : IRequest<TransactionModel>
    {
        public long Id { get; set; }
    }
}
