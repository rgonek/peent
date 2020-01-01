﻿using FluentValidation.TestHelper;
using Peent.Application.Accounts.Queries.GetAccount;
using Xunit;

namespace Peent.UnitTests.Accounts
{
    public class GetAccountQueryValidatorTests
    {
        private readonly GetAccountQueryValidator _validator;

        public GetAccountQueryValidatorTests()
        {
            _validator = new GetAccountQueryValidator();
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
        public void when_name_is_greater_than_0__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);
        }
    }
}