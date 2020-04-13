using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
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
            var existingCurrency = await _db.Currencies
                .SingleOrDefaultAsync(x =>
                    x.Code == command.Code,
                    token);

            if (existingCurrency != null)
                throw DuplicateException.Create<Currency>(x => x.Code, command.Code);

            var currency = new Currency(
                command.Code,
                command.Name,
                command.Symbol,
                command.DecimalPlaces);

            _db.Currencies.Add(currency);

            await _db.SaveChangesAsync(token);

            return currency.Id;
        }
    }
}
