using System.Collections.Generic;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using TransactionEntryEntity = Peent.Domain.Entities.TransactionAggregate.TransactionEntry;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.Transaction
{
    public class when_two_entries_added_
    {
        [Fact]
        public void one_with_asset_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<TransactionEntryEntity>>(new List<TransactionEntryEntity>
                {
                    CreateTransactionEntry(AccountType.Asset),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.TransactionAggregate.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.TransactionAggregate.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Transfer);
        }

        [Fact]
        public void one_with_asset_account_and_the_second_one_with_expense_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<TransactionEntryEntity>>(new List<TransactionEntryEntity>
                {
                    CreateTransactionEntry(AccountType.Asset),
                    CreateTransactionEntry(AccountType.Expense)
                }, nameof(Peent.Domain.Entities.TransactionAggregate.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.TransactionAggregate.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Withdrawal);
        }

        [Fact]
        public void one_with_revenue_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<TransactionEntryEntity>>(new List<TransactionEntryEntity>
                {
                    CreateTransactionEntry(AccountType.Revenue),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.TransactionAggregate.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.TransactionAggregate.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Deposit);
        }

        [Fact]
        public void one_with_initial_balance_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<TransactionEntryEntity>>(new List<TransactionEntryEntity>
                {
                    CreateTransactionEntry(AccountType.InitialBalance),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.TransactionAggregate.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.TransactionAggregate.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.OpeningBalance);
        }

        [Fact]
        public void one_with_reconciliation_account_and_the_second_one_with_asset_account()
        {
            var customizer = new FixedConstructorParameter<IEnumerable<TransactionEntryEntity>>(new List<TransactionEntryEntity>
                {
                    CreateTransactionEntry(AccountType.Reconciliation),
                    CreateTransactionEntry(AccountType.Asset)
                }, nameof(Peent.Domain.Entities.TransactionAggregate.Transaction.Entries).FirstDown());
            var transaction = Create<Peent.Domain.Entities.TransactionAggregate.Transaction>(customizer);

            transaction.Type.Should().Be(TransactionType.Reconciliation);
        }

        private TransactionEntryEntity CreateTransactionEntry(AccountType type)
        {
            var customizer = new FixedConstructorParameter<AccountType>(
                type, nameof(Peent.Domain.Entities.Account.Type).FirstDown());

            return new TransactionEntryEntity(Create<Peent.Domain.Entities.Account>(customizer), 1, 1);
        }
    }
}
