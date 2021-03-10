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
    public class UniqueInCurrentContextValidator<T, TEntity> :
        AsyncValidatorBase,
        IUniqueInCurrentContextValidator
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _userAccessor;
        private readonly Func<T, Expression<Func<TEntity, bool>>> _predicate;

        public UniqueInCurrentContextValidator(
            IApplicationDbContext db,
            ICurrentContextService userAccessor,
            Func<T, Expression<Func<TEntity, bool>>> predicate)
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(userAccessor, nameof(userAccessor)).IsNotNull();
            Ensure.That(predicate, nameof(predicate)).IsNotNull();

            _db = db;
            _userAccessor = userAccessor;
            _predicate = predicate;
        }

        protected override string GetDefaultMessageTemplate() => "Entity \"{EntityName}\" ({PropertyValue}) already exists.";

        protected override async Task<bool> IsValidAsync(
            PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var entityType = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", entityType.Name);

            var query = _db.Set<TEntity>().Where(_predicate((T)context.InstanceToValidate));

            if (typeof(IHaveWorkspace).IsAssignableFrom(entityType))
            {
                query = query
                    .OfType<IHaveWorkspace>()
                    .Where(x => x.Workspace.Id == _userAccessor.Workspace.Id)
                    .OfType<TEntity>();
            }

            return await query.AnyAsync(cancellation) == false;
        }
    }
}