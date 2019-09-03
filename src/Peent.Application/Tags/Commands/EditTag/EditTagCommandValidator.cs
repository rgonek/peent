using FluentValidation;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommandValidator : AbstractValidator<EditTagCommand>
    {
        public EditTagCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
