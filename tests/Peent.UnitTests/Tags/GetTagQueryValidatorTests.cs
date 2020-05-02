using FluentValidation.TestHelper;
using Peent.Application.Tags.Queries.GetTag;
using Peent.UnitTests.Common.Fakes.Validators;
using Xunit;

namespace Peent.UnitTests.Tags
{
    public class GetTagQueryValidatorTests
    {
        private readonly GetTagQueryValidator
            _validator = new GetTagQueryValidator(new AlwaysExistsValidatorProvider());

        [Fact]
        public void when_id_is_0__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);

        [Fact]
        public void when_id_is_negative__should_have_error()
            => _validator.ShouldHaveValidationErrorFor(x => x.Id, -1);

        [Fact]
        public void when_id_is_greater_than_0__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);

        [Fact]
        public void when_exists__should_not_have_error()
            => _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);

        [Fact]
        public void when_does_not_exists__should_have_error()
            => new GetTagQueryValidator(new AlwaysNotExistsValidatorProvider())
                .ShouldHaveValidationErrorFor(x => x.Id, 1);
    }
}