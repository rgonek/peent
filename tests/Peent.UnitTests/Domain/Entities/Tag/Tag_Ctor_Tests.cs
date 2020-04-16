using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Tag;

namespace Peent.UnitTests.Domain.Entities.Tag
{
    public class Tag_Ctor_Tests
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

            var tag = Create<Sut>(customizer);

            tag.Name.Should().Be(name);
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

            var tag = fixture.Create<Sut>();

            tag.Description.Should().Be(description);
        }

        [Fact]
        public void when_date_is_null__does_not_throw()
        {
            DateTime? date = null;
            var parameterName = nameof(Sut.Date).FirstDown();
            var customizer = new FixedConstructorParameter<DateTime?>(
                date, parameterName);
            var fixture = Fixture(customizer);
            fixture.Customize<Sut>(c =>
                c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));

            var tag = fixture.Create<Sut>();

            tag.Date.Should().Be(date);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void when_workspace_id_is_not_positive__throws_argument_exception(int currencyId)
        {
            var parameterName = nameof(Sut.WorkspaceId).FirstDown();
            var customizer = new FixedConstructorParameter<int>(
                currencyId, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_workspace_id_is_positive__does_not_throw()
        {
            var workspaceId = F.Create<int>();
            var customizer = new FixedConstructorParameter<int>(
                workspaceId, nameof(Sut.WorkspaceId).FirstDown());

            var tag = Create<Sut>(customizer);

            tag.WorkspaceId.Should().Be(workspaceId);
        }

        [Fact]
        public void when_all_parameters_are_valid__correctly_set_properties()
        {
            var name = F.Create<string>();
            var description = F.Create<string>();
            var date = F.Create<DateTime?>();
            var workspaceId = F.Create<int>();

            var tag = new Sut(name, description, date, workspaceId);

            tag.Name.Should().Be(name);
            tag.Description.Should().Be(description);
            tag.Date.Should().Be(date);
            tag.WorkspaceId.Should().Be(workspaceId);
        }
    }
}
