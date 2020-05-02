using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
    {
        public GetTagQueryValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            var val = exists.In<Tag>();
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(val);
        }
    }
}