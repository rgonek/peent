using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Currency : Entity<int>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public ushort DecimalPlaces { get; private set; }

        private Currency() { }

        public Currency(string code, string name, string symbol, ushort decimalPlaces)
        {
            SetCode(code);
            SetName(name);
            SetSymbol(symbol);
            SetDecimalPlaces(decimalPlaces);
        }

        public void SetCode(string code)
        {
            Ensure.That(code, nameof(code)).IsNotNullOrWhiteSpace();

            Code = code;
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetSymbol(string symbol)
        {
            Ensure.That(symbol, nameof(symbol)).IsNotNullOrWhiteSpace();

            Symbol = symbol;
        }

        public void SetDecimalPlaces(ushort decimalPlaces)
        {
            Ensure.That(decimalPlaces, nameof(decimalPlaces)).IsPositive();

            DecimalPlaces = decimalPlaces;
        }
    }
}
