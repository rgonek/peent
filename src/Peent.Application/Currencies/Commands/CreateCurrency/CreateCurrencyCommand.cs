using MediatR;

namespace Peent.Application.Currencies.Commands.CreateCurrency
{
    public class CreateCurrencyCommand : IRequest<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public ushort DecimalPlaces { get; set; }
    }
}
