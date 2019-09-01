using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Categories.Queries.GetCategory;
using Xunit;
using static Peent.UnitTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Categories
{
    public class GetCategoryQueryValidatorTests
    {
        private readonly GetCategoryQueryValidator _validator;

        public GetCategoryQueryValidatorTests()
        {
            _validator = new GetCategoryQueryValidator();
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
