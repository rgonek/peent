using FluentValidation;

namespace Peent.Application.Infrastructure
{
    public class PageInfoValidator : AbstractValidator<PageInfo>
    {
        public PageInfoValidator()
        {
            RuleFor(x => x.Page)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.PageSize)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
