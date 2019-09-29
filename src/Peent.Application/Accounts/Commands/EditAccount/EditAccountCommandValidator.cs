using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Accounts.Commands.EditAccount
{
    public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
    {
        private readonly IUniqueChecker _uniqueChecker;
        private readonly IUserAccessor _userAccessor;

        public EditAccountCommandValidator(IUniqueChecker uniqueChecker, IUserAccessor userAccessor)
        {
            _uniqueChecker = uniqueChecker;
            _userAccessor = userAccessor;

            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
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

        private async Task<bool> HasUniqueName(EditAccountCommand command,
            string name, CancellationToken cancellationToken)
        {
            return await _uniqueChecker.IsUniqueAsync<Account>(x =>
                    x.Id != command.Id &&
                    x.Name == name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionDate.HasValue == false,
                cancellationToken);
        }
    }
}
