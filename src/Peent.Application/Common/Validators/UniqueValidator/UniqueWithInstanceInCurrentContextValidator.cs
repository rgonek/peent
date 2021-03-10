using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Peent.Domain.Common;

namespace Peent.Application.Common.Validators.UniqueValidator
{
    public class UniqueWithInstanceInCurrentContextValidator<T, TEntity> :
        AsyncValidatorBase,
        IUniqueInCurrentContextValidator
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _currentContextService;
        private readonly Func<T, object> _propertyFinder;
        private readonly Func<TEntity, T, Expression<Func<TEntity, bool>>> _predicate;

        public UniqueWithInstanceInCurrentContextValidator(
            IApplicationDbContext db,
            ICurrentContextService currentContextService,
            Func<T, object> propertyFinder,
            Func<TEntity, T, Expression<Func<TEntity, bool>>> predicate)
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(currentContextService, nameof(currentContextService)).IsNotNull();
            Ensure.That(propertyFinder, nameof(propertyFinder)).IsNotNull();
            Ensure.That(predicate, nameof(predicate)).IsNotNull();

            _db = db;
            _currentContextService = currentContextService;
            _propertyFinder = propertyFinder;
            _predicate = predicate;
        }

        protected override string GetDefaultMessageTemplate() => "Entity \"{EntityName}\" ({PropertyValue}) already exists.";

        protected override async Task<bool> IsValidAsync(
            PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var entityType = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", entityType.Name);
            var instance = (T)context.InstanceToValidate;

            var entityInstance = await _db.Set<TEntity>().GetAsync(_propertyFinder(instance), cancellation);
            var query = _db.Set<TEntity>().Where(_predicate(entityInstance, instance));

            if (typeof(IHaveWorkspace).IsAssignableFrom(entityType))
            {
                query = query
                    .OfType<IHaveWorkspace>()
                    .Where(x => x.Workspace.Id == _currentContextService.Workspace.Id)
                    .OfType<TEntity>();
            }

            return await query.AnyAsync(cancellation) == false;
        }
    }
}