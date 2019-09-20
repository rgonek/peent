using FluentValidation;
using Peent.Application.Infrastructure;

namespace Peent.Application.Transactions.Queries.GetTransactionsList
{
    public class GetTransactionsListQueryValidator : AbstractValidator<GetTransactionsListQuery>
    {
        public GetTransactionsListQueryValidator()
        {
            RuleFor(x => x.PageInfo)
                .SetValidator(new PageInfoValidator());
        }
    }
}
