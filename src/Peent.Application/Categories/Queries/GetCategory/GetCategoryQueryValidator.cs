using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetCategoryQueryValidator(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .MustExistsInAuthenticationContext(typeof(Category), db, userAccessor);
        }
    }
}
