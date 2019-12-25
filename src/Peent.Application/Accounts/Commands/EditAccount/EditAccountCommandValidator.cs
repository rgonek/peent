using FluentValidation;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
    {
        public EditAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
