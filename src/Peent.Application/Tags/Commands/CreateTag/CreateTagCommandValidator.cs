using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        private readonly IUniqueChecker _uniqueChecker;
        private readonly IUserAccessor _userAccessor;

        public CreateTagCommandValidator(IUniqueChecker uniqueChecker, IUserAccessor userAccessor)
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
        }

        private async Task<bool> HasUniqueName(CreateTagCommand command,
            string name, CancellationToken cancellationToken)
        {
            return await _uniqueChecker.IsUniqueAsync<Tag>(x =>
                    x.Name == name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionInfo.DeletionDate.HasValue == false,
                cancellationToken);
        }
    }
}
