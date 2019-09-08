using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class CreateAccountCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_account_with_given_name_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();

            await ValidateAsync<CreateAccountCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_account_with_given_name_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            await SendAsync(command);

            await ValidateAsync<CreateAccountCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_account_with_given_name_exists_in_another_workspace__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            await SendAsync(command);

            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));

            await ValidateAsync<CreateAccountCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        [Fact]
        public async Task when_account_with_given_name_exists_but_is_deleted__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);
            await SendAsync(new DeleteAccountCommand { Id = accountId });


            await ValidateAsync<CreateAccountCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, command));
        }

        private CreateAccountCommand GetCreateAccountCommand()
        {
            return F.Build<CreateAccountCommand>()
                .With(x => x.CurrencyId, _currencyId.Value)
                .Create();
        }

        private static int? _currencyId;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (_currencyId.HasValue == false)
                _currencyId = await SendAsync(F.Create<CreateCurrencyCommand>());
        }
    }
}
