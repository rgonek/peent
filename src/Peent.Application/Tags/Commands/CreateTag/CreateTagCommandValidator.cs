using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator(IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000)
                .Must(beUnique.In<Tag>()
                    .WhereNot<CreateTagCommand>(cmd => x => x.Name == cmd.Name));
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
