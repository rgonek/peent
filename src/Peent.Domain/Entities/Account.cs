using System;
using EnsureThat;
using Peent.Domain.Common;

namespace Peent.Domain.Entities
{
    public class Account : AuditableEntity, IEntity<int>
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public int WorkspaceId { get; private set; }
        public Workspace Workspace { get; private set; }

        public AccountType Type { get; private set; }

        public int CurrencyId { get; private set; }
        public Currency Currency { get; private set; }

        #region Ctors
        private Account() { }

        public Account(string name, AccountType type, int currencyId, int workspaceId)
            : this(name, null, type, currencyId, workspaceId)
        {
        }

        #endregion

        public Account(string name, string description, AccountType type, int currencyId, int workspaceId)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();
            Ensure.That(type, nameof(type)).IsNotDefault();
            Ensure.That(workspaceId, nameof(workspaceId)).IsPositive();
            Ensure.That(currencyId, nameof(currencyId)).IsPositive();

            Name = name;
            Description = description;
            WorkspaceId = workspaceId;
            Type = type;
            CurrencyId = currencyId;
        }

        public void SetName(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrWhiteSpace();

            Name = name;
        }

        public void SetDescription(string description) => Description = description;

        public void SetCurrency(int currencyId)
        {
            Ensure.That(currencyId, nameof(currencyId)).IsPositive();

            CurrencyId = currencyId;
        }
    }
}
