using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator(
            IExistsInCurrentContextValidatorProvider exists,
            IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000)
                .Must(beUnique.In<Account>()
                    .WhereNot<CreateAccountCommand>(cmd => x =>
                        x.Name == cmd.Name &&
                        x.Type == cmd.Type));
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