using System.Collections.Generic;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Domain.Entities.Transaction
{
    public class when_two_entries_added_
    {
        [Fact]
        public void one_with_asset_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionEntry>>(new List<Peent.Domain.Entities.TransactionEntry>
                {
                    CreateTransactionEntry(AccountType.Asset),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Transfer);
        }

        [Fact]
        public void one_with_asset_account_and_the_second_one_with_expense_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionEntry>>(new List<Peent.Domain.Entities.TransactionEntry>
                {
                    CreateTransactionEntry(AccountType.Asset),
                    CreateTransactionEntry(AccountType.Expense)
                }, nameof(Peent.Domain.Entities.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Withdrawal);
        }

        [Fact]
        public void one_with_revenue_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionEntry>>(new List<Peent.Domain.Entities.TransactionEntry>
                {
                    CreateTransactionEntry(AccountType.Revenue),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Deposit);
        }

        [Fact]
        public void one_with_initial_balance_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionEntry>>(new List<Peent.Domain.Entities.TransactionEntry>
                {
                    CreateTransactionEntry(AccountType.InitialBalance),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.OpeningBalance);
        }

        [Fact]
        public void one_with_reconciliation_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<Peent.Domain.Entities.TransactionEntry>>(new List<Peent.Domain.Entities.TransactionEntry>
                {
                    CreateTransactionEntry(AccountType.Reconciliation),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Reconciliation);
        }

        private Peent.Domain.Entities.TransactionEntry CreateTransactionEntry(AccountType type)
        {
            var customizer = new FixedConstructorParameter<AccountType>(
                type, nameof(Peent.Domain.Entities.Account.Type).FirstDown());

            return new Peent.Domain.Entities.TransactionEntry(Create<Peent.Domain.Entities.Account>(customizer), 1, 1);
        }
    }
}
