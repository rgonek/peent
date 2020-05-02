using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.CreateCurrency
{
    public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
    {
        public CreateCurrencyCommandValidator(IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(3)
                .Must(beUnique.In<Currency>()
                    .WhereNot<CreateCurrencyCommand>(cmd => x => x.Code == cmd.Code));
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(12);
            RuleFor(x => x.DecimalPlaces)
                .NotNull()
                .LessThanOrEqualTo((ushort) 18);
        }
    }
}