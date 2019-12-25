using MediatR;

namespace Peent.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
