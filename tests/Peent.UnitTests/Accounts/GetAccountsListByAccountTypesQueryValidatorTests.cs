using FluentValidation.TestHelper;
using Peent.Application.Accounts.Queries.GetAccountsListByAccountTypes;
using Peent.Domain.Entities;
using Xunit;

namespace Peent.UnitTests.Accounts
{
    public class GetAccountsListByAccountTypesQueryValidatorTests
    {
        private readonly GetAccountsListByAccountTypesQueryValidator _validator;

        public GetAccountsListByAccountTypesQueryValidatorTests()
        {
            _validator = new GetAccountsListByAccountTypesQueryValidator();
        }

        [Fact]
        public void when_types_array_is_null__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Types, (AccountType[])null);
        }

        [Fact]
        public void when_types_array_is_empty__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Types, new AccountType[0]);
        }

        [Fact]
        public void when_types_array_contains_only_unknown_types__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Types, new[] { AccountType.Unknown, AccountType.Unknown });
        }

        [Theory]
        [InlineData(AccountType.Asset)]
        [InlineData(AccountType.Cash)]
        [InlineData(AccountType.Debt)]
        [InlineData(AccountType.Expense)]
        [InlineData(AccountType.InitialBalance)]
        [InlineData(AccountType.Loan)]
        [InlineData(AccountType.Mortgage)]
        [InlineData(AccountType.Reconciliation)]
        [InlineData(AccountType.Revenue)]
        public void when_types_array_contains_unknown_type_among_other_valid_types__should_have_error(AccountType validType)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Types, new[] { validType, AccountType.Unknown });
        }

        [Theory]
        [InlineData(AccountType.Asset)]
        [InlineData(AccountType.Cash)]
        [InlineData(AccountType.Debt)]
        [InlineData(AccountType.Expense)]
        [InlineData(AccountType.InitialBalance)]
        [InlineData(AccountType.Loan)]
        [InlineData(AccountType.Mortgage)]
        [InlineData(AccountType.Reconciliation)]
        [InlineData(AccountType.Revenue)]
        public void when_types_array_contains_not_unknown_types__should_have_error(AccountType type)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Types, new[] { type });
        }

        [Theory]
        [InlineData(AccountType.Asset)]
        [InlineData(AccountType.Cash)]
        [InlineData(AccountType.Debt)]
        [InlineData(AccountType.Expense)]
        [InlineData(AccountType.InitialBalance)]
        [InlineData(AccountType.Loan)]
        [InlineData(AccountType.Mortgage)]
        [InlineData(AccountType.Reconciliation)]
        [InlineData(AccountType.Revenue)]
        public void when_types_array_contains_duplicated_not_unknown_types__should_have_error(AccountType type)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Types, new[] { type, type });
        }
    }
}
