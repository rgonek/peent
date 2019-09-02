using FluentValidation;
using Peent.Application.Tags.Queries.GetTag;

namespace Peent.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandValidator : AbstractValidator<GetTagQuery>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
