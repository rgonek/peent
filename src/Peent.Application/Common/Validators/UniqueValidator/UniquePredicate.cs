using System;
using System.Linq.Expressions;
using EnsureThat;
using FluentValidation.Validators;

namespace Peent.Application.Common.Validators.UniqueValidator
{
    public class UniquePredicate<TEntity> : IUniquePredicate<TEntity>
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _userAccessor;

        public UniquePredicate(IApplicationDbContext db, ICurrentContextService userAccessor)
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(userAccessor, nameof(userAccessor)).IsNotNull();

            _db = db;
            _userAccessor = userAccessor;
        }

        public IPropertyValidator WhereNot<T>(Func<T, Expression<Func<TEntity, bool>>> predicate)
            => new UniqueInCurrentContextValidator<T, TEntity>(_db, _userAccessor, predicate);

        public IPropertyValidator WhereNot<T>(
            Func<T, object> propertyFinder,
            Func<TEntity, T, Expression<Func<TEntity, bool>>> predicate)
            => new UniqueWithInstanceInCurrentContextValidator<T, TEntity>(_db, _userAccessor, propertyFinder, predicate);
    }
}