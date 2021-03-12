using FluentValidation.Validators;

namespace Peent.UnitTests.Common.Fakes.Validators
{
    public sealed class AlwaysTrueValidator : PropertyValidator
    {
        public AlwaysTrueValidator()
        {
        }

        protected override bool IsValid(PropertyValidatorContext context) => true;
    }
}