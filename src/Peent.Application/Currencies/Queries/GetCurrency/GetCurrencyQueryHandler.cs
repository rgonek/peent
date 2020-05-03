using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Currencies.Models;

namespace Peent.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, CurrencyModel>
    {
        private readonly IApplicationDbContext _db;

        public GetCurrencyQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<CurrencyModel> Handle(GetCurrencyQuery query, CancellationToken token)
        {
            var currency = await _db.Currencies.GetAsync(query.Id, token);

            return new CurrencyModel(currency);
        }
    }
}