using FluentValidation;
using FluentValidation.Validators;

namespace Peent.Application.Common.Validators
{
    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, TElement> Must<T,TElement>(
            this IRuleBuilder<T, TElement> ruleBuilder, IPropertyValidator validator)
        {
            return ruleBuilder.SetValidator(validator);
        }
    }
}