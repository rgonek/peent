using FluentValidation;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryValidator : AbstractValidator<GetTagsListQuery>
    {
        public GetTagsListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
