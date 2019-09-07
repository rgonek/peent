using FluentValidation;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.Type)
                .NotEmpty();
            RuleFor(x => x.CurrencyId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
