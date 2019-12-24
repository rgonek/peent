using FluentValidation;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryValidator : AbstractValidator<GetCategoriesListQuery>
    {
        public GetCategoriesListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
