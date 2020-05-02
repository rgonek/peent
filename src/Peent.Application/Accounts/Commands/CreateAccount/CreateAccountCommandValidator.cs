using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .GreaterThan(0)
                .Must(exists.In<Currency>());
        }
    }
}