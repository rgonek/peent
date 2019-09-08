using System.Threading;
using FluentValidation.TestHelper;
using Peent.Application.Tags.Commands.EditTag;
using Peent.CommonTests.Infrastructure;
using Xunit;
using AutoFixture;
using FakeItEasy;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;
using static Peent.UnitTests.Infrastructure.TestFixture;

namespace Peent.UnitTests.Tags
{
    public class EditTagCommandValidatorTests
    {
        private readonly EditTagCommandValidator _validator;

        public EditTagCommandValidatorTests()
        {
            var uniqueChecker = A.Fake<IUniqueChecker>();
            A.CallTo(() => uniqueChecker.IsUniqueAsync<Tag>(null, CancellationToken.None))
                .WithAnyArguments()
                .Returns(true);
            _validator = new EditTagCommandValidator(
                uniqueChecker,
                A.Fake<IUserAccessor>());
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
