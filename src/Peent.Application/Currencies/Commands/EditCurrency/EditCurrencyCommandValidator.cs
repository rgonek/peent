using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommandValidator : AbstractValidator<EditCurrencyCommand>
    {
        public EditCurrencyCommandValidator(
            IExistsInCurrentContextValidatorProvider exists,
            IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Currency>());
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(3)
                .Must(beUnique.In<Currency>()
                    .WhereNot<EditCurrencyCommand>(cmd => x =>
                        x.Id != cmd.Id &&
                        x.Code == cmd.Code));
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