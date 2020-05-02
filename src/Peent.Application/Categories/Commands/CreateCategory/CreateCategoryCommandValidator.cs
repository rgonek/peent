using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000)
                .Must(beUnique.In<Category>()
                    .WhereNot<CreateCategoryCommand>(cmd => x =>
                        x.Name == cmd.Name));
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}