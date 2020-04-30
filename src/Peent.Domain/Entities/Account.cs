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

        public int WorkspaceId { get; private set; }

        #region Ctors
        private Account() { }

        public Account(string name, AccountType type, Currency currency, int workspaceId)
            : this(name, null, type, currency, workspaceId)
        {
        }

        #endregion

        public Account(string name, string description, AccountType type, Currency currency, int workspaceId)
        {
            Ensure.That(type, nameof(type)).IsNotDefault();
            Ensure.That(workspaceId, nameof(workspaceId)).IsPositive();

            SetName(name);
            SetDescription(description);
            SetCurrency(currency);
            WorkspaceId = workspaceId;
            Type = type;
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
