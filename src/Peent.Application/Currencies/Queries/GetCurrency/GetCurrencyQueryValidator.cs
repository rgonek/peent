using FluentValidation;

namespace Peent.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQueryValidator : AbstractValidator<GetCurrencyQuery>
    {
        public GetCurrencyQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
