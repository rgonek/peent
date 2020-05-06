using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Category;

namespace Peent.UnitTests.Domain.Entities.Category
{
    public class Category_Ctor_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_name_is_null_or_white_space__throws_argument_exception(string name)
        {
            var parameterName = nameof(Sut.Name).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                name, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_name_is_not_null_or_white_space__does_not_throw()
        {
            var name = F.Create<string>();
            var customizer = new FixedConstructorParameter<string>(
                name, nameof(Sut.Name).FirstDown());

            var account = Create<Sut>(customizer);

            account.Name.Should().Be(name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_description_is_null_or_white_space__does_not_throw(string description)
        {
            var parameterName = nameof(Sut.Description).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                description, parameterName);
            var fixture = Fixture(customizer);
            fixture.Customize<Sut>(c =>
                c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));

            var account = fixture.Create<Sut>();

            account.Description.Should().Be(description);
        }

        [Fact]
        public void when_all_parameters_are_valid__correctly_set_properties()
        {
            var name = F.Create<string>();
            var description = F.Create<string>();

            var account = new Sut(name, description);

            account.Name.Should().Be(name);
            account.Description.Should().Be(description);
        }
    }
}
