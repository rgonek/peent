using System;
using System.Linq;
using AutoFixture.Kernel;
using EnumsNET;
using Peent.CommonTests.Infrastructure;
using Peent.Domain.Entities;

namespace Peent.CommonTests.AutoFixture
{
    public class ValidRandomAccountTypeSequenceGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var t = request as Type;
            if (t == null || !t.IsEnum || t != typeof(AccountType))
            {
                return new NoSpecimen();
            }

            var values = new[] {AccountType.Asset};
            var randomIndex = StaticRandom.Next(0, values.Length);
            var result = values[randomIndex];
            return result;
        }
    }
}