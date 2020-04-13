using EnsureThat;

namespace Peent.Domain.Entities
{
    public class Currency
    {
        public int Id { get; private set; }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public ushort DecimalPlaces { get; private set; }

        private Currency() { }

        public Currency(string code, string name, string symbol, ushort decimalPlaces)
        {
            Ensure.That(code, nameof(code)).IsNotNullOrWhiteSpace();
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();
            Ensure.That(symbol, nameof(symbol)).IsNotNullOrWhiteSpace();
            Ensure.That(decimalPlaces, nameof(decimalPlaces)).IsPositive();

            Code = code;
            Name = name;
            Symbol = symbol;
            DecimalPlaces = decimalPlaces;
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
