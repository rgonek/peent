using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Application.Currencies.Models;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, CurrencyModel>
    {
        private readonly IApplicationDbContext _db;

        public GetCurrencyQueryHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CurrencyModel> Handle(GetCurrencyQuery query, CancellationToken token)
        {
            var currency = await _db.Currencies
                .SingleOrDefaultAsync(x => x.Id == query.Id,
                    cancellationToken: token);

            if (currency == null)
                throw NotFoundException.Create<Currency>(x => x.Id, query.Id);

            return new CurrencyModel(currency);
        }
    }
}
