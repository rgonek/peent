using System.Collections.Generic;
using MediatR;
using Peent.Application.Currencies.Models;

namespace Peent.Application.Currencies.Queries.GetCurrenciesList
{
    public class GetCurrenciesListQuery : IRequest<IList<CurrencyModel>>
    {
    }
}
