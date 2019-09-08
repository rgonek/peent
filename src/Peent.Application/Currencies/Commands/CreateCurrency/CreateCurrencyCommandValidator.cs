using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.CreateCurrency
{
    public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
    {
        private readonly IUniqueChecker _uniqueChecker;

        public CreateCurrencyCommandValidator(IUniqueChecker uniqueChecker)
        {
            _uniqueChecker = uniqueChecker;

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

        private async Task<bool> HasUniqueCode(CreateCurrencyCommand command,
            string code, CancellationToken cancellationToken)
        {
            return await _uniqueChecker.IsUniqueAsync<Currency>(x =>
                    x.Code == code,
                cancellationToken);
        }
    }
}
