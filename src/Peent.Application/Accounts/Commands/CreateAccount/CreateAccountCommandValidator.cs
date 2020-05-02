using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.CurrencyId)
                .MustExistsInAuthenticationContext(typeof(Currency), db, userAccessor)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}