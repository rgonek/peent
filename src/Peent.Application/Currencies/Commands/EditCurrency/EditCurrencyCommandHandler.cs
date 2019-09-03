﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Exceptions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Currencies.Commands.EditCurrency
{
    public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommand, Unit>
    {
        private readonly IApplicationDbContext _db;

        public EditCurrencyCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(EditCurrencyCommand command, CancellationToken token)
        {
            var currency = await _db.Currencies
                .SingleOrDefaultAsync(x =>
                        x.Id == command.Id,
                    token);

            if (currency == null)
                throw NotFoundException.Create<Currency>(x => x.Id, command.Id);

            var existingCurrency = await _db.Currencies
                .SingleOrDefaultAsync(x =>
                    x.Id != command.Id &&
                    x.Code == command.Code,
                    token);

            if (existingCurrency != null)
                throw DuplicateException.Create<Currency>(x => x.Code, command.Code);

            currency.Code = command.Code;
            currency.Name = command.Name;
            currency.Symbol = command.Symbol;
            currency.DecimalPlaces = command.DecimalPlaces;

            _db.Update(currency);
            await _db.SaveChangesAsync(token);

            return default;
        }
    }
}