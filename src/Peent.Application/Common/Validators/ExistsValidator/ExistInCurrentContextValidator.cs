using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using FluentValidation.Validators;
using Peent.Application.Common.Extensions;
using Peent.Domain.Common;

namespace Peent.Application.Common.Validators.ExistsValidator
{
    public class ExistsInCurrentContextValidator<TEntity> : AsyncValidatorBase,
        IExistsInCurrentContextValidator<TEntity>
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public ExistsInCurrentContextValidator(IApplicationDbContext db, IUserAccessor userAccessor)
            : base("Entity \"{EntityName}\" ({PropertyValue}) was not found.")
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(userAccessor, nameof(userAccessor)).IsNotNull();

            _db = db;
            _userAccessor = userAccessor;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var typeEntity = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", typeEntity.Name);
            var entity = await _db.Set<TEntity>().GetAsync(context.PropertyValue, cancellation);
            if (entity is null)
            {
                return false;
            }

            if (typeof(IHaveWorkspace).IsAssignableFrom(typeEntity) == false)
            {
                return true;
            }

            var entityWithWorkspace = entity as IHaveWorkspace;
            await _db.Entry(entityWithWorkspace).Reference(x => x.Workspace).LoadAsync(cancellation);

            return entityWithWorkspace.Workspace == _userAccessor.User.GetWorkspace();
        }
    }
}