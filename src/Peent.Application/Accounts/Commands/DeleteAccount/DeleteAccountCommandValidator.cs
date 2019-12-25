using FluentValidation;
using Peent.Application.Accounts.Queries.GetAccount;

namespace Peent.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandValidator : AbstractValidator<GetAccountQuery>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
