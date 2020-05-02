using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQueryValidator : AbstractValidator<GetCurrencyQuery>
    {
        public GetCurrencyQueryValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Currency>());
        }
    }
}
