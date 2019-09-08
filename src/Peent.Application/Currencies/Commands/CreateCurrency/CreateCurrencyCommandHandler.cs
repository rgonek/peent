using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.CreateCurrency
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, int>
    {
        private readonly IApplicationDbContext _db;

        public CreateCurrencyCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(CreateCurrencyCommand command, CancellationToken token)
        {
            var currency = new Currency
            {
                Code = command.Code,
                Name = command.Name,
                Symbol = command.Symbol,
                DecimalPlaces = command.DecimalPlaces
            };

            _db.Currencies.Add(currency);

            await _db.SaveChangesAsync(token);

            return currency.Id;
        }
    }
}
