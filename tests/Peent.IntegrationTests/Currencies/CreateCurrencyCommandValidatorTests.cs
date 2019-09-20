using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Currencies
{
    public class CreateCurrencyCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_currency_with_given_code_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Build<CreateCurrencyCommand>()
                .With(x => x.Code, F.Create<string>().Substring(0, 3))
                .Create();

            await ValidateAsync<CreateCurrencyCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Code, command));
        }

        [Fact]
        public async Task when_currency_with_given_code_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = F.Create<CreateCurrencyCommand>();
            await SendAsync(command);

            await ValidateAsync<CreateCurrencyCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Code, command));
        }
    }
}
