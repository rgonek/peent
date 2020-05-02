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
        private readonly IUserAccessor _userAccessor;

        public UniquePredicate(IApplicationDbContext db, IUserAccessor userAccessor)
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(userAccessor, nameof(userAccessor)).IsNotNull();

            _db = db;
            _userAccessor = userAccessor;
        }

        public IPropertyValidator WhereNot<T>(Func<T, Expression<Func<TEntity, bool>>> predicate)
            => new UniqueInCurrentContextValidator<T, TEntity>(_db, _userAccessor, predicate);
    }
}