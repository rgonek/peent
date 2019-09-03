using FluentValidation;

namespace Peent.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
