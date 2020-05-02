using FluentValidation.TestHelper;
using Peent.Application.Currencies.Commands.DeleteCurrency;
using Xunit;

namespace Peent.UnitTests.Currencies
{
    public class DeleteCurrencyCommandValidatorTests
    {
        private readonly DeleteCurrencyCommandValidator _validator;

        public DeleteCurrencyCommandValidatorTests()
        {
//            _validator = new DeleteCurrencyCommandValidator();
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
    }
}
