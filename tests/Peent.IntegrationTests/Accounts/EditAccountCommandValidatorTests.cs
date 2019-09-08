using System.Threading.Tasks;
using AutoFixture;
using FluentValidation.TestHelper;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.IntegrationTests.Infrastructure;
using Xunit;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Accounts
{
    public class EditAccountCommandValidatorTests : IntegrationTestBase
    {
        [Fact]
        public async Task when_account_with_given_name_not_exists__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);
            var editCommand = new EditAccountCommand
            {
                Id = accountId,
                Name = F.Create<string>(),
                CurrencyId = _currencyId.Value
            };

            await ValidateAsync<EditAccountCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_account_with_given_name_exists__should_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);
            var command2 = GetCreateAccountCommand();
            await SendAsync(command2);
            var editCommand = new EditAccountCommand
            {
                Id = accountId,
                Name = command2.Name,
                CurrencyId = _currencyId.Value
            };

            await ValidateAsync<EditAccountCommandValidator>(validator =>
                validator.ShouldHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_account_with_given_name_exists_in_another_workspace__should_not_have_error()
        {
            var user = await CreateUserAsync();
            SetCurrentUser(user, await CreateWorkspaceAsync(user));
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);
            var command2 = GetCreateAccountCommand();
            var accountId2 = await SendAsync(command2);
            await SendAsync(new DeleteAccountCommand { Id = accountId2 });
            var editCommand = new EditAccountCommand
            {
                Id = accountId,
                Name = command2.Name,
                CurrencyId = _currencyId.Value
            };

            await ValidateAsync<EditAccountCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
        }

        [Fact]
        public async Task when_account_with_given_name_exists_but_is_deleted__should_not_have_error()
        {
            var user = await CreateUserAsync();
            var workspace = await CreateWorkspaceAsync(user);
            SetCurrentUser(user, workspace);
            var command = GetCreateAccountCommand();
            var accountId = await SendAsync(command);
            var user2 = await CreateUserAsync();
            SetCurrentUser(user2, await CreateWorkspaceAsync(user2));
            var command2 = GetCreateAccountCommand();
            await SendAsync(command2);
            SetCurrentUser(user, workspace);
            var editCommand = new EditAccountCommand
            {
                Id = accountId,
                Name = command2.Name,
                CurrencyId = _currencyId.Value
            };

            await ValidateAsync<EditAccountCommandValidator>(validator =>
                validator.ShouldNotHaveValidationErrorFor(x => x.Name, editCommand));
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
