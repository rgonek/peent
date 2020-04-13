using FluentValidation.TestHelper;
using Xunit;
using AutoFixture;
using Peent.Application.Transactions.Commands.CreateTransaction;
using Peent.CommonTests.AutoFixture;
using Peent.CommonTests.Infrastructure;
using static Peent.CommonTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Transactions
{
    public class CreateTransactionCommandValidatorTests
    {
        private readonly CreateTransactionCommandValidator _validator;

        public CreateTransactionCommandValidatorTests()
        {
            _validator = new CreateTransactionCommandValidator();
        }

        [Fact]
        public void when_name_is_null__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Title, null as string);
        }

        [Fact]
        public void when_name_is_empty__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty);
        }

        [Fact]
        public void when_name_is_specified__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Title, F.Create<string>());
        }

        [Fact]
        public void when_name_is_1000_characters_long__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Title, F.CreateString(1000));
        }

        [Fact]
        public void when_name_is_longer_than_1000_characters__should_have_error()
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
        public void when_source_account_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.SourceAccountId, 0);
        }

        [Fact]
        public void when_source_account_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.SourceAccountId, -1);
        }

        [Fact]
        public void when_source_account_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.SourceAccountId, 1);
        }

        [Fact]
        public void when_destination_account_id_is_0__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DestinationAccountId, 0);
        }

        [Fact]
        public void when_destination_account_id_is_negative__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DestinationAccountId, -1);
        }

        [Fact]
        public void when_destination_account_id_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.DestinationAccountId, 1);
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
    }
}
