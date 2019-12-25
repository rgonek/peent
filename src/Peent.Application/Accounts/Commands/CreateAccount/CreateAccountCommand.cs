using MediatR;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountType Type { get; set; }
        public int CurrencyId { get; set; }
    }
}
