using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
    {
        public EditAccountCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Account), db, userAccessor);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Currency), db, userAccessor);
        }                
    }
}
