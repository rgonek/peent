using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQueryValidator : AbstractValidator<GetCurrencyQuery>
    {
        public GetCurrencyQueryValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Currency), db, userAccessor);
        }
    }
}
