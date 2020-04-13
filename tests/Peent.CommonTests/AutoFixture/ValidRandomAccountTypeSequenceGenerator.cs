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

            var values = Enums.GetValues<AccountType>()
                .Except(new[] { AccountType.Unknown })
                .ToList();
            var randomIndex = StaticRandom.Next(0, values.Count);
            var result = values[randomIndex];
            return result;
        }
    }
}
