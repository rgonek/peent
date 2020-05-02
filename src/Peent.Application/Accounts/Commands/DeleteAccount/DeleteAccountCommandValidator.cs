using FluentValidation;
using Peent.Application.Accounts.Queries.GetAccount;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandValidator : AbstractValidator<GetAccountQuery>
    {
        public DeleteAccountCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Account), db, userAccessor);
        }
    }
}
