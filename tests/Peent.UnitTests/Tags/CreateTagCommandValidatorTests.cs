using System.Threading;
using FluentValidation.TestHelper;
using Peent.Application.Tags.Commands.CreateTag;
using Xunit;
using AutoFixture;
using FakeItEasy;
using Peent.Application.Interfaces;
using Peent.CommonTests.Infrastructure;
using Peent.Domain.Entities;
using static Peent.UnitTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Tags
{
    public class CreateTagCommandValidatorTests
    {
        private readonly CreateTagCommandValidator _validator;

        public CreateTagCommandValidatorTests()
        {
            var uniqueChecker = A.Fake<IUniqueChecker>();
            A.CallTo(() => uniqueChecker.IsUniqueAsync<Tag>(null, CancellationToken.None))
                .WithAnyArguments()
                .Returns(true);
            _validator = new CreateTagCommandValidator(
                uniqueChecker,
                A.Fake<IUserAccessor>());
        }

        [Fact]
        public void when_name_is_null__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
        }

        [Fact]
        public void when_name_is_empty__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [Fact]
        public void when_name_is_specified__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, F.Create<string>());
        }

        [Fact]
        public void when_name_is_1000_characters_long__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, F.CreateString(1000));
        }

        [Fact]
        public void when_name_is_longer_than_1000_characters__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, F.CreateString(1001));
        }

        [Fact]
        public void when_description_is_null__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, null as string);
        }

        [Fact]
        public void when_description_is_specified__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, F.Create<string>());
        }

        [Fact]
        public void when_description_is_2000_characters_long__should_not_have_error()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, F.CreateString(2000));
        }

        [Fact]
        public void when_description_is_longer_than_2000_characters__should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Description, F.CreateString(2001));
        }
    }
}
