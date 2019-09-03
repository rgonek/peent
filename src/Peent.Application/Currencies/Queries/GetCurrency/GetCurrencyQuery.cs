using MediatR;
using Peent.Application.Currencies.Models;

namespace Peent.Application.Currencies.Queries.GetCurrency
{
    public class GetCurrencyQuery : IRequest<CurrencyModel>
    {
        public int Id { get; set; }
    }
}
