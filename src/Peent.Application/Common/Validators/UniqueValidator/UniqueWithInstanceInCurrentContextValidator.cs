using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Peent.Application.Common.Extensions;
using Peent.Domain.Common;

namespace Peent.Application.Common.Validators.UniqueValidator
{
    public class UniqueWithInstanceInCurrentContextValidator<T, TEntity> : AsyncValidatorBase,
        IUniqueInCurrentContextValidator
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;
        private readonly Func<T, object> _propertyFinder;
        private readonly Func<TEntity, T, Expression<Func<TEntity, bool>>> _predicate;

        public UniqueWithInstanceInCurrentContextValidator(
            IApplicationDbContext db,
            IUserAccessor userAccessor,
            Func<T, object> propertyFinder,
            Func<TEntity, T, Expression<Func<TEntity, bool>>> predicate)
            : base("Entity \"{EntityName}\" ({PropertyValue}) already exists.")
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(userAccessor, nameof(userAccessor)).IsNotNull();
            Ensure.That(propertyFinder, nameof(propertyFinder)).IsNotNull();
            Ensure.That(predicate, nameof(predicate)).IsNotNull();

            _db = db;
            _userAccessor = userAccessor;
            _propertyFinder = propertyFinder;
            _predicate = predicate;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var typeEntity = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", typeEntity.Name);
            var instance = (T) context.Instance;

            var entityInstance = await _db.Set<TEntity>().GetAsync(_propertyFinder(instance), cancellation);

            var query = _db.Set<TEntity>().Where(_predicate(entityInstance, instance));

            if (typeof(IHaveWorkspace).IsAssignableFrom(typeEntity))
            {
                query = query
                    .OfType<IHaveWorkspace>()
                    .Where(x => x.Workspace.Id == _userAccessor.User.GetWorkspace().Id)
                    .OfType<TEntity>();
            }

            return await query.AnyAsync(cancellation) == false;
        }
    }
}