using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Tag), db, userAccessor);
        }
    }
}
