using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
    {
        public EditAccountCommandValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Account>());
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .GreaterThan(0)
                .Must(exists.In<Currency>());
        }                
    }
}
