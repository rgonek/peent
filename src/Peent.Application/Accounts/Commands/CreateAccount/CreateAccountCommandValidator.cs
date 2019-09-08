using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly IUniqueChecker _uniqueChecker;
        private readonly IUserAccessor _userAccessor;

        public CreateAccountCommandValidator(IUniqueChecker uniqueChecker,
            IUserAccessor userAccessor)
        {
            _uniqueChecker = uniqueChecker;
            _userAccessor = userAccessor;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(1000)
                .MustAsync(HasUniqueName);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
            RuleFor(x => x.Type)
                .NotEmpty();
            RuleFor(x => x.CurrencyId)
                .NotNull()
                .GreaterThan(0);
        }

        private async Task<bool> HasUniqueName(CreateAccountCommand command,
            string name, CancellationToken cancellationToken)
        {
            return await _uniqueChecker.IsUniqueAsync<Account>(x =>
                    x.Name == name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionInfo.DeletionDate.HasValue == false,
                cancellationToken);
        }
    }
}
