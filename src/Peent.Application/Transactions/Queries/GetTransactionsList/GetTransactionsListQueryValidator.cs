using FluentValidation;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQueryValidator : AbstractValidator<GetTransactionsListQuery>
    {
        public GetTransactionsListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
