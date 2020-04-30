using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Peent.Domain.Entities.TransactionAggregate;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using TransactionEntity = Peent.Domain.Entities.TransactionAggregate.Transaction;
using AccountEntity = Peent.Domain.Entities.Account;

namespace Peent.UnitTests.Domain.Entities.TransactionAggregate.Transaction
{
    public class when_transaction_is_created_type_is_initialized
    {
        [Fact]
        public void one_with_asset_account_and_the_second_one_with_asset_account()
        {
            var transaction = CreateTransaction(AccountType.Asset, AccountType.Asset);

            transaction.Type.Should().Be(TransactionType.Transfer);
        }

        [Fact]
        public void one_with_asset_account_and_the_second_one_with_expense_account()
        {
            var transaction = CreateTransaction(AccountType.Asset, AccountType.Expense);

            transaction.Type.Should().Be(TransactionType.Withdrawal);
        }

        [Fact]
        public void one_with_revenue_account_and_the_second_one_with_asset_account()
        {
            var transaction = CreateTransaction(AccountType.Revenue, AccountType.Asset);

            transaction.Type.Should().Be(TransactionType.Deposit);
        }

        [Fact]
        public void one_with_initial_balance_account_and_the_second_one_with_asset_account()
        {
            var transaction = CreateTransaction(AccountType.InitialBalance, AccountType.Asset);

            transaction.Type.Should().Be(TransactionType.OpeningBalance);
        }

        [Fact]
        public void one_with_reconciliation_account_and_the_second_one_with_asset_account()
        {
            var transaction = CreateTransaction(AccountType.Reconciliation, AccountType.Asset);

            transaction.Type.Should().Be(TransactionType.Reconciliation);
        }

        private TransactionEntity CreateTransaction(AccountType fromAccountType, AccountType toAccountType)
        {
            var fromCustomizer = new FixedConstructorParameter<AccountEntity>(CreateAccount(fromAccountType), "fromAccount");
            var toCustomizer = new FixedConstructorParameter<AccountEntity>(CreateAccount(toAccountType), "toAccount");
            return Create<Peent.Domain.Entities.TransactionAggregate.Transaction>(fromCustomizer, toCustomizer);
        }

        private AccountEntity CreateAccount(AccountType type)
        {
            var customizer = new FixedConstructorParameter<AccountType>(
                type, nameof(Peent.Domain.Entities.Account.Type).FirstDown());

            return Create<AccountEntity>(customizer);
        }
    }
}
