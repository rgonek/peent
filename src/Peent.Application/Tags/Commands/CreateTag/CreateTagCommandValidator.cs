using FluentValidation;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
