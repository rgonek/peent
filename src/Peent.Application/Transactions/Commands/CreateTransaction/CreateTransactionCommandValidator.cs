using FluentValidation;

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
                .NotNull();

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.FromAccountId)
                .NotEmpty()
                .GreaterThan(0)
                .NotEqual(x => x.ToAccountId);

            RuleFor(x => x.ToAccountId)
                .NotEmpty()
                .GreaterThan(0)
                .NotEqual(x => x.FromAccountId);

            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
