using AutoFixture;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.Common;
using Peent.Domain.Entities;

namespace Peent.CommonTests.AutoFixture
{
    public static class AutoFixtureConfiguration
    {
        public static void Configure(this Fixture fixture)
        {
            fixture.Customize<CreateCurrencyCommand>(ct => ct
                .With(x => x.Code, () => fixture.Create<string>().Substring(0, 3))
                .With(x => x.Name, fixture.Create<string>)
                .With(x => x.Symbol, () => fixture.Create<string>().Substring(0, 12))
                .With(x => x.DecimalPlaces, fixture.Create<ushort>)
            );
            fixture.Customize<EditCurrencyCommand>(ct => ct
                .With(x => x.Code, () => fixture.Create<string>().Substring(0, 3))
                .With(x => x.Name, fixture.Create<string>)
                .With(x => x.Symbol, () => fixture.Create<string>().Substring(0, 12))
                .With(x => x.DecimalPlaces, fixture.Create<ushort>)
            );

            fixture.ConstructorArgumentFor<Currency, string>(nameof(Currency.Code).FirstDown(), fixture.CreateString(3))
                .ConstructorArgumentFor<Currency, string>(nameof(Currency.Symbol).FirstDown(),
                    fixture.CreateString(12));

            fixture.Customizations.Insert(0, new ValidRandomAccountTypeSequenceGenerator());
        }
    }
}