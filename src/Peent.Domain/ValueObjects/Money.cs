using System.Collections.Generic;
using EnsureThat;
using Peent.Domain.Common;
using Peent.Domain.Entities;

namespace Peent.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        private Money()
        {
        }

        public Money(decimal amount, Currency currency)
        {
            Ensure.That(amount, nameof(amount)).IsNotZero();
            Ensure.That(currency, nameof(currency)).IsNotNull();

            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}