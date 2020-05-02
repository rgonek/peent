using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.CreateCurrency
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public CreateCurrencyCommandHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<int> Handle(CreateCurrencyCommand command, CancellationToken token)
        {
            var currency = new Currency(
                command.Code,
                command.Name,
                command.Symbol,
                command.DecimalPlaces);

            _db.Currencies.Attach(currency);
            await _db.SaveChangesAsync(token);

            return currency.Id;
        }
    }
}