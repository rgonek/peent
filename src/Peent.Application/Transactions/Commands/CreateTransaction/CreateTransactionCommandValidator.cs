using FluentValidation;
using Peent.Domain.Entities;

namespace Peent.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.Date)
                .NotEmpty();
            RuleFor(x => x.Type)
                .NotEmpty()
                .NotEqual(TransactionType.Unknown);
            RuleFor(x => x.CategoryId)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.CurrencyId)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.FromAccountId)
                .NotNull()
                .GreaterThan(0)
                .NotEqual(x => x.ToAccountId);
            RuleFor(x => x.ToAccountId)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.ForeignAmount)
                .GreaterThan(0)
                .When(x => x.ForeignAmount.HasValue);
            RuleFor(x => x.ForeignCurrencyId)
                .GreaterThan(0)
                .When(x => x.ForeignCurrencyId.HasValue);

            RuleFor(x => x.ForeignAmount)
                .NotEmpty()
                .GreaterThan(0)
                .When(x => x.ForeignCurrencyId.HasValue);
            RuleFor(x => x.ForeignCurrencyId)
                .NotEmpty()
                .GreaterThan(0)
                .When(x => x.ForeignAmount.HasValue);
        }
    }
}
