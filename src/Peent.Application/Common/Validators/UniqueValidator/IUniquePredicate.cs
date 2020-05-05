using System;
using System.Linq.Expressions;
using FluentValidation.Validators;

namespace Peent.Application.Common.Validators.UniqueValidator
{
    public interface IUniquePredicate<TEntity>
        where TEntity : class
    {
        IPropertyValidator WhereNot<T>(Func<T, Expression<Func<TEntity, bool>>> predicate);
        IPropertyValidator WhereNot<T>(
            Func<T, object> propertyFinder,
            Func<TEntity, T, Expression<Func<TEntity, bool>>> predicate);
    }
}