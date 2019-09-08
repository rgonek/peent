using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommandValidator : AbstractValidator<EditCurrencyCommand>
    {
        private readonly IUniqueChecker _uniqueChecker;

        public EditCurrencyCommandValidator(IUniqueChecker uniqueChecker)
        {
            _uniqueChecker = uniqueChecker;

            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(3)
                .MustAsync(HasUniqueCode);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(12);
            RuleFor(x => x.DecimalPlaces)
                .NotNull()
                .LessThanOrEqualTo((ushort)18);
        }

        private async Task<bool> HasUniqueCode(EditCurrencyCommand command,
            string code, CancellationToken cancellationToken)
        {
            return await _uniqueChecker.IsUniqueAsync<Currency>(x =>
                    x.Id != command.Id &&
                    x.Code == code,
                    cancellationToken);
        }
    }
}
