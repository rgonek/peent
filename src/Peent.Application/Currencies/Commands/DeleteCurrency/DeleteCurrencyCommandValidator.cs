using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Currencies.Queries.GetCurrency;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommandValidator : AbstractValidator<GetCurrencyQuery>
    {
        public DeleteCurrencyCommandValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Currency>());
        }
    }
}
