using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandValidator : AbstractValidator<EditTagCommand>
    {
        public EditTagCommandValidator(
            IExistsInCurrentContextValidatorProvider exists,
            IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Currency>());
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000)
                .Must(beUnique.In<Tag>()
                    .WhereNot<EditTagCommand>(cmd => x => x.Name == cmd.Name));
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
