using FluentValidation.Validators;

namespace Peent.UnitTests.Common.Fakes.Validators
{
    public sealed class AlwaysFalseValidator : PropertyValidator
    {
        public AlwaysFalseValidator() : base("Always false")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context) => false;
    }
}