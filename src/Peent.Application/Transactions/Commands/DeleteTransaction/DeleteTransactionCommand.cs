using MediatR;

namespace Peent.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
