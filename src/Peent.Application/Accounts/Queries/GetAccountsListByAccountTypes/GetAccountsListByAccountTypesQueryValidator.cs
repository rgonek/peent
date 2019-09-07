using System.Linq;
using FluentValidation;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Queries.GetAccountsListByAccountTypes
{
    public class GetAccountsListByAccountTypesQueryValidator : AbstractValidator<GetAccountsListByAccountTypesQuery>
    {
        public GetAccountsListByAccountTypesQueryValidator()
        {
            RuleFor(x => x.Types)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(x => x.Any(y => y != AccountType.Unknown))
                .WithMessage("Types should contains at least one type other than unknown.");
        }
    }
}
