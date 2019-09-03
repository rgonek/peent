using MediatR;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public ushort DecimalPlaces { get; set; }
    }
}
