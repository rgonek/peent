using FluentValidation.Validators;

namespace Peent.UnitTests.Common.Fakes.Validators
{
    public sealed class AlwaysFalseValidator : PropertyValidator
    {
        protected override string GetDefaultMessageTemplate() => "Always false";

        protected override bool IsValid(PropertyValidatorContext context) => false;
    }
}