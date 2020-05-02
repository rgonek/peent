using FluentValidation;
using Peent.Application.Common.Validators;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
    {
        public GetTagQueryValidator(IApplicationDbContext db, IUserAccessor userAccessor)//, IExistsInAuthenticationContextValidator<Tag> validator)
        {
//            IPropertyValidator<Tag> validator = new ExistInAuthenticationContextValidator<Tag>(db, userAccessor);
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
//                .SetValidator(validator)
                .MustExistsInAuthenticationContext(typeof(Tag), db, userAccessor);
        }
    }
}
