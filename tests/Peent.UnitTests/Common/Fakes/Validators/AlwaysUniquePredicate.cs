using System;
using System.Linq.Expressions;
using FluentValidation.Validators;
using Peent.Application.Common.Validators.UniqueValidator;

namespace Peent.UnitTests.Common.Fakes.Validators
{
    public class AlwaysUniquePredicate<TEntity> : IUniquePredicate<TEntity>
        where TEntity : class
    {
        public IPropertyValidator WhereNot<T>(Func<T, Expression<Func<TEntity, bool>>> predicate)
            => new AlwaysTrueValidator();

        public IPropertyValidator WhereNot<T>(Func<T, object> propertyFinder,
            Func<TEntity, T, Expression<Func<TEntity, bool>>> predicate)
            => new AlwaysTrueValidator();
    }
}