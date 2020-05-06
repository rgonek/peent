using System;
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Account : AuditableEntity<int>, IHaveWorkspace
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public AccountType Type { get; private set; }

        public Currency Currency { get; private set; }

        public Workspace Workspace { get; private set; }

        #region Ctors

        private Account()
        {
        }

        public Account(string name, AccountType type, Currency currency)
            : this(name, null, type, currency)
        {
        }

        #endregion

        public Account(string name, string description, AccountType type, Currency currency)
        {
            Ensure.That(type, nameof(type)).IsNotDefault();

            SetName(name);
            SetDescription(description);
            SetCurrency(currency);
            Type = type;
        }

        public void SetWorkspace(Workspace workspace)
        {
            if (Workspace != null)
            {
//                throws
            }

            Workspace = workspace;
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetDescription(string description) => Description = description;

        public void SetCurrency(Currency currency)
        {
            Ensure.That(currency, nameof(currency)).IsNotNull();

            Currency = currency;
        }
    }
}