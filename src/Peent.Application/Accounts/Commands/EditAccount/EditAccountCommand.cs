using MediatR;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrencyId { get; set; }
    }
}
