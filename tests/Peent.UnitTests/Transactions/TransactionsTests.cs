using FluentAssertions;
using Peent.Domain.Entities;
using Xunit;

namespace Peent.UnitTests.Transactions
{
    public class TransactionsTests
    {
        public class when_two_entries_aded_
        {
            [Fact]
            public void one_with_asset_account_and_the_second_one_with_asset_account()
            {
                var transaction = new Transaction();
                AddEntryOfAccountType(transaction, AccountType.Asset);
                AddEntryOfAccountType(transaction, AccountType.Asset);

                transaction.Type.Should().Be(TransactionType.Transfer);
            }

            [Fact]
            public void one_with_asset_account_and_the_second_one_with_expense_account()
            {
                var transaction = new Transaction();
                AddEntryOfAccountType(transaction, AccountType.Asset);
                AddEntryOfAccountType(transaction, AccountType.Expense);

                transaction.Type.Should().Be(TransactionType.Withdrawal);
            }

            [Fact]
            public void one_with_revenue_account_and_the_second_one_with_asset_account()
            {
                var transaction = new Transaction();
                AddEntryOfAccountType(transaction, AccountType.Revenue);
                AddEntryOfAccountType(transaction, AccountType.Asset);

                transaction.Type.Should().Be(TransactionType.Deposit);
            }

            [Fact]
            public void one_with_initial_balance_account_and_the_second_one_with_asset_account()
            {
                var transaction = new Transaction();
                AddEntryOfAccountType(transaction, AccountType.InitialBalance);
                AddEntryOfAccountType(transaction, AccountType.Asset);

                transaction.Type.Should().Be(TransactionType.OpeningBalance);
            }

            [Fact]
            public void one_with_reconciliation_account_and_the_second_one_with_asset_account()
            {
                var transaction = new Transaction();
                AddEntryOfAccountType(transaction, AccountType.Reconciliation);
                AddEntryOfAccountType(transaction, AccountType.Asset);

                transaction.Type.Should().Be(TransactionType.Reconciliation);
            }

            private void AddEntryOfAccountType(Transaction transaction, AccountType type)
            {
                transaction.AddEntry(new Account { Type = type }, 0, 0);
            }
        }
    }
}
