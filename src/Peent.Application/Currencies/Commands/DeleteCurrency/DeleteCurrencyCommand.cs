using MediatR;

namespace Peent.Application.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
