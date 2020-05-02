using FluentValidation.TestHelper;
using Peent.Application.Currencies.Queries.GetCurrency;
using Peent.UnitTests.Common.Fakes.Validators;
using Xunit;

namespace Peent.UnitTests.Currencies
{
    public class GetCurrencyQueryValidatorTests
    {
        private readonly GetCurrencyQueryValidator _validator =
            new GetCurrencyQueryValidator(new AlwaysExistsValidatorProvider());

        [Fact]
        public void when_id_is_0__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);

        [Fact]
        public void when_id_is_negative__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Id, -1);

        [Fact]
        public void when_name_is_greater_than_0__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);

        [Fact]
        public void when_exists__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);

        [Fact]
        public void when_does_not_exists__should_have_error()
            => new GetCurrencyQueryValidator(new AlwaysNotExistsValidatorProvider())
                .ShouldHaveValidationErrorFor(x => x.Id, 1);
    }
}