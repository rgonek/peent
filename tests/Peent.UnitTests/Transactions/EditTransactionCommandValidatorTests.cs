using FluentValidation.TestHelper;
using Xunit;
using AutoFixture;
using Peent.CommonTests.Infrastructure;
using Peent.Domain.Entities;
using static Peent.UnitTests.Infrastructure.TestFixture;
using Peent.Application.Transactions.Commands.EditTransaction;

namespace Peent.UnitTests.Transactions
{
    public class EditTransactionCommandValidatorTests
    {
        private readonly EditTransactionCommandValidator _validator;

        public EditTransactionCommandValidatorTests()
        {
            _validator = new EditTransactionCommandValidator();
        }

        [Fact]
        public void when_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }

        [Fact]
        public void when_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, -1);
        }

        [Fact]
        public void when_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);
        }

        [Fact]
        public void when_title_is_null__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Title, null as string);
        }

        [Fact]
        public void when_title_is_empty__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty);
        }

        [Fact]
        public void when_title_is_specified__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Title, F.Create<string>());
        }

        [Fact]
        public void when_title_is_1000_characters_long__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Title, F.CreateString(1000));
        }

        [Fact]
        public void when_title_is_longer_than_1000_characters__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Title, F.CreateString(1001));
        }

        [Fact]
        public void when_description_is_null__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, null as string);
        }

        [Fact]
        public void when_description_is_specified__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, F.Create<string>());
        }

        [Fact]
        public void when_description_is_2000_characters_long__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, F.CreateString(2000));
        }

        [Fact]
        public void when_description_is_longer_than_2000_characters__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, F.CreateString(2001));
        }

        [Fact]
        public void when_currency_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CurrencyId, 0);
        }

        [Fact]
        public void when_currency_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CurrencyId, -1);
        }

        [Fact]
        public void when_currency_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.CurrencyId, 1);
        }

        [Fact]
        public void when_category_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CategoryId, 0);
        }

        [Fact]
        public void when_category_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CategoryId, -1);
        }

        [Fact]
        public void when_category_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.CategoryId, 1);
        }

        [Fact]
        public void when_from_account_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.FromAccountId, 0);
        }

        [Fact]
        public void when_from_account_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.FromAccountId, -1);
        }

        [Fact]
        public void when_from_account_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.FromAccountId, 1);
        }

        [Fact]
        public void when_to_account_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ToAccountId, 0);
        }

        [Fact]
        public void when_to_account_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ToAccountId, -1);
        }

        [Fact]
        public void when_to_account_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ToAccountId, 1);
        }

        [Fact]
        public void when_amount_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }

        [Fact]
        public void when_amount_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, -1);
        }

        [Fact]
        public void when_amount_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Amount, 1);
        }

        [Fact]
        public void when_foreign_currency_id_is_null__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ForeignCurrencyId, null as int?);
        }

        [Fact]
        public void when_foreign_currency_is_zero__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ForeignCurrencyId, 0);
        }

        [Fact]
        public void when_foreign_currency_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ForeignCurrencyId, -1);
        }

        [Fact]
        public void when_foreign_currency_is_positive__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ForeignCurrencyId, 1);
        }

        [Fact]
        public void when_foreign_amount_is_null__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ForeignCurrencyId, null as int?);
        }

        [Fact]
        public void when_foreign_amount_is_zero__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ForeignCurrencyId, 0);
        }

        [Fact]
        public void when_foreign_amount_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ForeignCurrencyId, -1);
        }

        [Fact]
        public void when_foreign_amount_is_positive__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ForeignCurrencyId, 1);
        }

        [Fact]
        public void when_foreign_currency_id_is_not_null_and_foreign_amount_is_null__should_have_error()
        {
            var command = new EditTransactionCommand
            {
                ForeignCurrencyId = 1,
                ForeignAmount = null
            };
            _validator.ShouldHaveValidationErrorFor(x => x.ForeignAmount, command);
        }

        [Fact]
        public void when_foreign_amount_is_not_null_and_foreign_currency_id_is_null__should_have_error()
        {
            var command = new EditTransactionCommand
            {
                ForeignCurrencyId = null,
                ForeignAmount = 1
            };
            _validator.ShouldHaveValidationErrorFor(x => x.ForeignCurrencyId, command);
        }

        [Fact]
        public void when_type_is_unknown__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Type, TransactionType.Unknown);
        }

        [Theory]
        [InlineData(TransactionType.Deposit)]
        [InlineData(TransactionType.OpeningBalance)]
        [InlineData(TransactionType.Reconciliation)]
        [InlineData(TransactionType.Transfer)]
        [InlineData(TransactionType.Withdrawal)]
        public void when_type_is__not_unknown__should_not_have_error(TransactionType type)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Type, type);
        }
    }
}
