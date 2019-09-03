using FluentValidation.TestHelper;
using Peent.Application.Categories.Commands.DeleteCategory;
using Xunit;

namespace Peent.UnitTests.Categories
{
    public class DeleteCategoryCommandValidatorTests
    {
        private readonly DeleteCategoryCommandValidator _validator;

        public DeleteCategoryCommandValidatorTests()
        {
            _validator = new DeleteCategoryCommandValidator();
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
