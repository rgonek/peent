using FluentValidation;
using FluentValidation.Validators;

namespace Peent.Application.Common.Validators
{
    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, IPropertyValidator validator)
            => ruleBuilder.SetValidator(validator);
    }
}