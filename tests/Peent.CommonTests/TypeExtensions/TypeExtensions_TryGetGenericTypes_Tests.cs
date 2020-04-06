using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Peent.Common;
using Xunit;

namespace Peent.CommonTests.TypeExtensions
{
    public class TypeExtensions_TryGetGenericTypes_Tests
    {
        [Fact]
        public void correctly_retrieve_generic_type()
        {
            typeof(ComplexType)
                .TryGetGenericType<IWrap, IWrap2, IEnumerable>(out var desiredType)
                .Should().BeTrue();

            desiredType.Should().Be(typeof(DesiredType));
        }

        private class ComplexType : Wrapper<Wrapper2<List<Wrapper<IList<Wrapper<ICollection<Wrapper2<DesiredType>>>>>>>>
        {
        }

        private class DesiredType
        {
        }

        private interface IWrap
        {
        }

        private interface IWrap<T> : IWrap
        {
        }

        private class Wrapper<T> : IWrap<T>
        {
        }

        private interface IWrap2
        {
        }

        private interface IWrap2<T> : IWrap2
        {
        }

        private class Wrapper2<T> : IWrap2<T>
        {
        }
    }
}
