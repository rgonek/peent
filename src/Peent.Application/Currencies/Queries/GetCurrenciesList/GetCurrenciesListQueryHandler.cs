using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Currencies.Models;

namespace Peent.Application.Currencies.Queries.GetCurrenciesList
{
    public class GetCurrenciesListQueryHandler : IRequestHandler<GetCurrenciesListQuery, IList<CurrencyModel>>
    {
        private readonly IApplicationDbContext _db;

        public GetCurrenciesListQueryHandler(IApplicationDbContext db)
            => _db = db;

        public async Task<IList<CurrencyModel>> Handle(GetCurrenciesListQuery query, CancellationToken token)
            => (await _db.Currencies
                    .ToListAsync(token))
                .Select(x => new CurrencyModel(x))
                .ToList();
    }
}
