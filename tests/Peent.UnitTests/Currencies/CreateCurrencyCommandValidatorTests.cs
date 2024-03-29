﻿using FluentValidation.TestHelper;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Xunit;
using AutoFixture;
using Peent.CommonTests.AutoFixture;
using Peent.UnitTests.Common.Fakes.Validators;
using static Peent.CommonTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Currencies
{
    public class CreateCurrencyCommandValidatorTests
    {
        private readonly CreateCurrencyCommandValidator _validator =
            new CreateCurrencyCommandValidator(new AlwaysUniqueValidatorProvider());
        
        [Fact]
        public void when_is_unique__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Code, F.CreateString(1));

        [Fact]
        public void when_is_duplicated__should_have_error()
            => new CreateCurrencyCommandValidator(new AlwaysDuplicatedValidatorProvider())
                .ShouldHaveValidationErrorFor(x => x.Code, F.CreateString(1));

        [Fact]
        public void when_code_is_null__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Code, null as string);

        [Fact]
        public void when_code_is_empty__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Code, string.Empty);

        [Fact]
        public void when_code_is_specified__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Code, F.CreateString(1));

        [Fact]
        public void when_code_is_3_characters_long__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Code, F.CreateString(3));

        [Fact]
        public void when_code_is_longer_than_3_characters__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Code, F.CreateString(4));

        [Fact]
        public void when_name_is_null__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);

        [Fact]
        public void when_name_is_empty__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);

        [Fact]
        public void when_name_is_specified__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Name, F.Create<string>());

        [Fact]
        public void when_name_is_255_characters_long__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Name, F.CreateString(255));

        [Fact]
        public void when_name_is_longer_than_255_characters__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Name, F.CreateString(256));

        [Fact]
        public void when_symbol_is_null__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Symbol, null as string);

        [Fact]
        public void when_symbol_is_empty__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Symbol, string.Empty);

        [Fact]
        public void when_symbol_is_specified__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Symbol, F.CreateString(1));

        [Fact]
        public void when_symbol_is_12_characters_long__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Symbol, F.CreateString(12));

        [Fact]
        public void when_symbol_is_longer_than_12_characters__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Symbol, F.CreateString(13));

        [Fact]
        public void when_decimal_places_is_0__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.DecimalPlaces, (ushort) 0);

        [Theory]
        [InlineData(1)]
        [InlineData(17)]
        [InlineData(18)]
        public void when_decimal_places_is_greater_than_0_and_less_than_or_equal_18__should_not_have_error(
            ushort decimalPlaces)
            => _validator.ShouldNotHaveValidationErrorFor(x => x.DecimalPlaces, decimalPlaces);

        [Fact]
        public void when_decimal_places_is_greater_than_18__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.DecimalPlaces, (ushort) 19);
    }
}