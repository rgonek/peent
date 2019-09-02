using FluentValidation.TestHelper;
using Peent.Application.Tags.Queries.GetTag;
using Xunit;

namespace Peent.UnitTests.Tags
{
    public class GetTagQueryValidatorTests
    {
        private readonly GetTagQueryValidator _validator;

        public GetTagQueryValidatorTests()
        {
            _validator = new GetTagQueryValidator();
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
