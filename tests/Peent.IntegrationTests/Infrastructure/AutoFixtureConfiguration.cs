using AutoFixture;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.Domain.Entities;

namespace Peent.IntegrationTests.Infrastructure
{
    public static class AutoFixtureConfiguration
    {
        public static void Configure(this Fixture fixture)
        {
            fixture.Customize<CreateCurrencyCommand>(ct => ct
                .With(x => x.Code, fixture.Create<string>().Substring(0, 3))
                .With(x => x.Name, fixture.Create<string>())
                .With(x => x.Symbol, fixture.Create<string>().Substring(0, 12))
                .With(x => x.DecimalPlaces, fixture.Create<ushort>())
            );
            fixture.Customize<EditCurrencyCommand>(ct => ct
                .With(x => x.Code, fixture.Create<string>().Substring(0, 3))
                .With(x => x.Name, fixture.Create<string>())
                .With(x => x.Symbol, fixture.Create<string>().Substring(0, 12))
                .With(x => x.DecimalPlaces, fixture.Create<ushort>())
            );
            fixture.Customize<CreateAccountCommand>(ct => ct
                .With(x => x.Name, fixture.Create<string>())
                .With(x => x.Description, fixture.Create<string>())
                .With(x => x.Type, fixture.Create<AccountType>())
            );
            fixture.Customize<EditAccountCommand>(ct => ct
                .With(x => x.Name, fixture.Create<string>())
                .With(x => x.Description, fixture.Create<string>())
                .With(x => x.Type, fixture.Create<AccountType>())
            );
        }
    }
}
