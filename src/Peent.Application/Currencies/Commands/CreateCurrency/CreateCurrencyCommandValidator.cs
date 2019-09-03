using FluentValidation;

namespace Peent.Application.Currencies.Commands.CreateCurrency
{
    public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
    {
        public CreateCurrencyCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(3);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(12);
            RuleFor(x => x.DecimalPlaces)
                .NotNull()
                .LessThanOrEqualTo((ushort)18);
        }
    }
}
