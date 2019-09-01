using FluentValidation;

namespace Peent.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetCategoryQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
