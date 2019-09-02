using FluentValidation;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
    {
        public GetTagQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
