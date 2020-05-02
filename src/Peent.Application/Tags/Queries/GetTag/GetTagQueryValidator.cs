using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
    {
        public GetTagQueryValidator(IExistsInCurrentContextValidatorProvider exists)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .Must(exists.In<Tag>());
        }
    }
}