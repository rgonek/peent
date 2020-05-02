using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Currencies.Queries.GetCurrency;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommandValidator : AbstractValidator<GetCurrencyQuery>
    {
        public DeleteCurrencyCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Currency), db, userAccessor);
        }
    }
}
