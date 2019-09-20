using MediatR;

namespace Peent.Application.Transactions.Commands.DeleteTransation
{
    public class DeleteTransactionCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
