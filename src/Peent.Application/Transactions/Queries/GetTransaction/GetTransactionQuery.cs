using MediatR;
using Peent.Application.Transactions.Models;

namespace Peent.Application.Transactions.Queries.GetTransaction
{
    public sealed class GetTransactionQuery : IRequest<TransactionModel>
    {
        public int Id { get; set; }
    }
}
