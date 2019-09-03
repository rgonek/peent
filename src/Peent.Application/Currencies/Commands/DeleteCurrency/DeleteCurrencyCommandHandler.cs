using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public DeleteCurrencyCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteCurrencyCommand command, CancellationToken token)
        {
            var currency = await _db.Currencies
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id,
                    token);

            if (currency == null)
                throw NotFoundException.Create<Currency>(x => x.Id, command.Id);

            _db.Remove(currency);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}
