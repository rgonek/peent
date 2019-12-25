using FluentValidation;

namespace Peent.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQueryValidator : AbstractValidator<GetAccountsListQuery>
    {
        public GetAccountsListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
