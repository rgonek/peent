using FluentValidation.TestHelper;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Xunit;

namespace Peent.UnitTests.Accounts
{
    public class DeleteAccountCommandValidatorTests
    {
        private readonly DeleteAccountCommandValidator _validator;

        public DeleteAccountCommandValidatorTests()
        {
//            _validator = new DeleteAccountCommandValidator();
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
