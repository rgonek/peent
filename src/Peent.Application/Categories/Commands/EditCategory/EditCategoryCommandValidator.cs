using FluentValidation;

namespace Peent.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
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
