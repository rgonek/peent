using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Peent.Application.Infrastructure.Extensions;
using Peent.Application.Interfaces;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        private readonly IUniqueChecker _uniqueChecker;
        private readonly IUserAccessor _userAccessor;

        public EditCategoryCommandValidator(IUniqueChecker uniqueChecker, IUserAccessor userAccessor)
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
        }

        private async Task<bool> HasUniqueName(EditCategoryCommand command,
            string name, CancellationToken cancellationToken)
        {
            return await _uniqueChecker.IsUniqueAsync<Category>(x =>
                    x.Id != command.Id &&
                    x.Name == name &&
                    x.WorkspaceId == _userAccessor.User.GetWorkspaceId() &&
                    x.DeletionDate.HasValue == false,
                cancellationToken);
        }
    }
}
