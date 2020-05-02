using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Application.Common.Validators.UniqueValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator(
            IExistsInCurrentContextValidatorProvider exists,
            IUniqueInCurrentContextValidatorProvider beUnique)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Category>());
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(1000)
                .Must(beUnique.In<Category>()
                    .WhereNot<EditCategoryCommand>(cmd => x =>
                        x.Id != cmd.Id &&
                        x.Name == cmd.Name));
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}