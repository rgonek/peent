using System.Threading.Tasks;
using Peent.Domain.Entities;
using AutoFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class CurrencyBuilder
    {
        private string _name;
        private string _code;
        private string _symbol;
        private ushort _decimalPlaces;

        public CurrencyBuilder WithRandomData()
        {
            _name = F.Create<string>();
            _code = F.Create<string>().Substring(0, 3);
            _symbol = F.Create<string>().Substring(0, 12);
            _decimalPlaces = F.Create<ushort>();
            return this;
        }

        public CurrencyBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CurrencyBuilder WithCode(string code)
        {
            _code = code;
            return this;
        }

        public CurrencyBuilder WithSymbol(string symbol)
        {
            _symbol = symbol;
            return this;
        }

        public CurrencyBuilder WithDecimalPlaces(ushort decimalPlaces)
        {
            _decimalPlaces = decimalPlaces;
            return this;
        }

        public async Task<Currency> Build()
        {
            var currency = new Currency
            {
                Name = _name,
                Code = _code,
                Symbol = _symbol,
                DecimalPlaces = _decimalPlaces
            };

            await InsertAsync(currency);

            return currency;
        }

        public static implicit operator Currency(CurrencyBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}
