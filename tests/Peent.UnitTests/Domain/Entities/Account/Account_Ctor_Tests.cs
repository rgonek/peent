using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using EnumsNET;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Peent.Domain.Entities;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Account;

namespace Peent.UnitTests.Domain.Entities.Account
{
    public class Account_Ctor_Tests
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
        public void when_type_is_of_unknown_type__throws_argument_exception()
        {
            var parameterName = nameof(Sut.Type).FirstDown();
            var customizer = new FixedConstructorParameter<AccountType>(
                AccountType.Unknown, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        public static IEnumerable<object[]> ValidAccountTypeValues()
        {
            foreach (var type in Enums.GetValues<AccountType>().Except(new[] { AccountType.Unknown }))
            {
                yield return new[] { (object)type };
            }
        }

        [Theory]
        [MemberData(nameof(ValidAccountTypeValues))]
        public void when_type_is_not_of_unknown_type__does_not_throw(AccountType type)
        {
            var customizer = new FixedConstructorParameter<AccountType>(
                type, nameof(Sut.Type).FirstDown());

            var account = Create<Sut>(customizer);

            account.Type.Should().Be(type);
        }

        [Fact]
        public void when_currency_is_null__throws_argument_exception()
        {
            Peent.Domain.Entities.Currency currency = null;
            var parameterName = nameof(Sut.Currency).FirstDown();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Currency>(
                currency, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_currency_is_not_null__does_not_throw()
        {
            var currency = F.Create<Peent.Domain.Entities.Currency>();
            var customizer = new FixedConstructorParameter<Peent.Domain.Entities.Currency>(
                currency, nameof(Sut.Currency).FirstDown());

            var account = Create<Sut>(customizer);

            account.Currency.Should().Be(currency);
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

            var account = Create<Sut>(customizer);

            account.WorkspaceId.Should().Be(workspaceId);
        }

        [Fact]
        public void when_all_parameters_are_valid__correctly_set_properties()
        {
            var name = F.Create<string>();
            var description = F.Create<string>();
            var type = F.Create<AccountType>();
            var currency = F.Create<Peent.Domain.Entities.Currency>();
            var workspaceId = F.Create<int>();

            var account = new Sut(name, description, type, currency, workspaceId);

            account.Name.Should().Be(name);
            account.Description.Should().Be(description);
            account.Type.Should().Be(type);
            account.Currency.Should().Be(currency);
            account.WorkspaceId.Should().Be(workspaceId);
        }
    }
}
