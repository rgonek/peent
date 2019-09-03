using FluentValidation;
using Peent.Application.Currencies.Queries.GetCurrency;

namespace Peent.Application.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommandValidator : AbstractValidator<GetCurrencyQuery>
    {
        public DeleteCurrencyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
