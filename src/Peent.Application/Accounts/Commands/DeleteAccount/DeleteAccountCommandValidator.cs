using FluentValidation;
using Peent.Application.Accounts.Queries.GetAccount;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandValidator : AbstractValidator<GetAccountQuery>
    {
        public DeleteAccountCommandValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Account>());
        }
    }
}
