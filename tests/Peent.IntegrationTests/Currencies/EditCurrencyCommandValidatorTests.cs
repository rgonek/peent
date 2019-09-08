using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    public class EditCurrencyCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_currency_with_given_code_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCurrencyCommand>();
            var currencyId = await SendAsync(command);
            var editCommand = F.Build<EditCurrencyCommand>()
                .With(x => x.Code, F.Create<string>().Substring(0, 3))
                .With(x => x.Id, currencyId)
                .Create();

            await ValidateAsync<EditCurrencyCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Code, editCommand));
        }

        [Fact]
        public async Task when_currency_with_given_code_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCurrencyCommand>();
            var currencyId = await SendAsync(command);
            var command2 = F.Create<CreateCurrencyCommand>();
            await SendAsync(command2);
            var editCommand = F.Build<EditCurrencyCommand>()
                .With(x => x.Id, currencyId)
                .With(x => x.Code, command2.Code)
                .Create();

            await ValidateAsync<EditCurrencyCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Code, editCommand));
        }
    }
}
