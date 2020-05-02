using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditCurrencyCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(EditCurrencyCommand command, CancellationToken token)
        {
            await ThrowsIfDuplicateAsync(command, token);

            var currency = await _db.Currencies.FindAsync(new[] {command.Id}, token);
            currency.SetCode(command.Code);
            currency.SetName(command.Name);
            currency.SetSymbol(command.Symbol);
            currency.SetDecimalPlaces(command.DecimalPlaces);

            _db.Attach(currency);
            await _db.SaveChangesAsync(token);

            return default;
        }

        private async Task ThrowsIfDuplicateAsync(EditCurrencyCommand command, CancellationToken token)
        {
            var existingCurrency = await _db.Currencies
                .SingleOrDefaultAsync(x =>
                        x.Id != command.Id &&
                        x.Code == command.Code,
                    token);

            if (existingCurrency != null)
            {
                throw DuplicateException.Create<Currency>(x => x.Code, command.Code);
            }
        }
    }
}