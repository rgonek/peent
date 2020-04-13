using AutoFixture.Kernel;

namespace Peent.CommonTests.AutoFixture
{
    public class FixedConstructorParameter<T> : FilteringSpecimenBuilder
    {
        public FixedConstructorParameter(T value, string targetName)
            : base(new FixedBuilder(value), new ParameterSpecification(typeof(T), targetName))
        {
        }
    }
}
