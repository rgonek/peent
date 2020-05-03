using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditCurrencyCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<Unit> Handle(EditCurrencyCommand command, CancellationToken token)
        {
            var currency = await _db.Currencies.GetAsync(command.Id, token);
            currency.SetCode(command.Code);
            currency.SetName(command.Name);
            currency.SetSymbol(command.Symbol);
            currency.SetDecimalPlaces(command.DecimalPlaces);

            _db.Attach(currency);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}