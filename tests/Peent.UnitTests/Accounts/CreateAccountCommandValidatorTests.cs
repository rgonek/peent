﻿using FluentValidation.TestHelper;
using Peent.Application.Accounts.Commands.CreateAccount;
using Xunit;
using AutoFixture;
using Peent.CommonTests.Infrastructure;
using Peent.Domain.Entities;
using static Peent.UnitTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Accounts
{
    public class CreateAccountCommandValidatorTests
    {
        private readonly CreateAccountCommandValidator _validator;

        public CreateAccountCommandValidatorTests()
        {
            _validator = new CreateAccountCommandValidator();
        }

        [Fact]
        public void when_name_is_null__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
        }

        [Fact]
        public void when_name_is_empty__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [Fact]
        public void when_name_is_specified__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, F.Create<string>());
        }

        [Fact]
        public void when_name_is_1000_characters_long__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, F.CreateString(1000));
        }

        [Fact]
        public void when_name_is_longer_than_1000_characters__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, F.CreateString(1001));
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
        public void when_type_is_unknown__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Type, AccountType.Unknown);
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
        public void when_type_is__not_unknown__should_not_have_error(AccountType type)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Type, type);
        }
    }
}