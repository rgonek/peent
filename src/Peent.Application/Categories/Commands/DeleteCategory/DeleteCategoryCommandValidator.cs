using FluentValidation;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<GetCategoryQuery>
    {
        public DeleteCategoryCommandValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Category), db, userAccessor);
        }
    }
}