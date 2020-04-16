using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Peent.Common;
using Peent.CommonTests.AutoFixture;
using Xunit;
using static Peent.CommonTests.Infrastructure.TestFixture;
using Sut = Peent.Domain.Entities.Currency;

namespace Peent.UnitTests.Domain.Entities.Currency
{
    public class Currency_Ctor_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void when_code_is_null_or_white_space__throws_argument_exception(string code)
        {
            var parameterName = nameof(Sut.Code).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                code, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_code_is_not_null_or_white_space__does_not_throw()
        {
            var code = F.Create<string>();
            var customizer = new FixedConstructorParameter<string>(
                code, nameof(Sut.Code).FirstDown());

            var account = Create<Sut>(customizer);

            account.Code.Should().Be(code);
        }

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
        public void when_symbol_is_null_or_white_space__throws_argument_exception(string symbol)
        {
            var parameterName = nameof(Sut.Symbol).FirstDown();
            var customizer = new FixedConstructorParameter<string>(
                symbol, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_symbol_is_not_null_or_white_space__does_not_throw()
        {
            var symbol = F.Create<string>();
            var customizer = new FixedConstructorParameter<string>(
                symbol, nameof(Sut.Symbol).FirstDown());

            var account = Create<Sut>(customizer);

            account.Symbol.Should().Be(symbol);
        }

        [Theory]
        [InlineData(0)]
        public void when_decimal_places_is_not_positive__throws_argument_exception(ushort decimalPlaces)
        {
            var parameterName = nameof(Sut.DecimalPlaces).FirstDown();
            var customizer = new FixedConstructorParameter<ushort>(
                decimalPlaces, parameterName);

            Action act = () => Create<Sut>(customizer);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"*{parameterName}*");
        }

        [Fact]
        public void when_decimal_places_id_is_positive__does_not_throw()
        {
            var decimalPlaces = F.Create<ushort>();
            var customizer = new FixedConstructorParameter<ushort>(
                decimalPlaces, nameof(Sut.DecimalPlaces).FirstDown());

            var account = Create<Sut>(customizer);

            account.DecimalPlaces.Should().Be(decimalPlaces);
        }

        [Fact]
        public void when_all_parameters_are_valid__correctly_set_properties()
        {
            var code = F.Create<string>();
            var name = F.Create<string>();
            var symbol = F.Create<string>();
            var decimalPlaces = F.Create<ushort>();

            var account = new Sut(code, name, symbol, decimalPlaces);

            account.Code.Should().Be(code);
            account.Name.Should().Be(name);
            account.Symbol.Should().Be(symbol);
            account.DecimalPlaces.Should().Be(decimalPlaces);
        }
    }
}
