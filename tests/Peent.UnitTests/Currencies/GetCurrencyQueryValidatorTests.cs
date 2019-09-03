using FluentValidation.TestHelper;
using Peent.Application.Currencies.Queries.GetCurrency;
using Xunit;

namespace Peent.UnitTests.Currencies
{
    public class GetCurrencyQueryValidatorTests
    {
        private readonly GetCurrencyQueryValidator _validator;

        public GetCurrencyQueryValidatorTests()
        {
            _validator = new GetCurrencyQueryValidator();
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
