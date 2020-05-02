using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandValidator : AbstractValidator<EditTagCommand>
    {
        public EditTagCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Tag), db, userAccessor);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
