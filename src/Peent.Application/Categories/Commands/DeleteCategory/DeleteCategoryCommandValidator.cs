using FluentValidation;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<GetCategoryQuery>
    {
        public DeleteCategoryCommandValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Category>());
        }
    }
}