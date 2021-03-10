using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using FluentValidation.Validators;
using Peent.Domain.Common;

namespace Peent.Application.Common.Validators.ExistsValidator
{
    public class ExistsInCurrentContextValidator<TEntity> :
        AsyncValidatorBase,
        IExistsInCurrentContextValidator<TEntity>
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _currentContextService;

        public ExistsInCurrentContextValidator(
            IApplicationDbContext db,
            ICurrentContextService currentContextService)
        {
            Ensure.That(db, nameof(db)).IsNotNull();
            Ensure.That(currentContextService, nameof(currentContextService)).IsNotNull();

            _db = db;
            _currentContextService = currentContextService;
        }

        protected override string GetDefaultMessageTemplate() => "Entity \"{EntityName}\" ({PropertyValue}) was not found.";

        protected override async Task<bool> IsValidAsync(
            PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var entityType = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", entityType.Name);

            var entity = await _db.Set<TEntity>().GetAsync(context.PropertyValue, cancellation);
            if (entity is null)
            {
                return false;
            }

            if (typeof(IHaveWorkspace).IsAssignableFrom(entityType) == false)
            {
                return true;
            }

            var entityWithWorkspace = entity as IHaveWorkspace;
            await _db.Entry(entityWithWorkspace).Reference(x => x.Workspace).LoadAsync(cancellation);

            return entityWithWorkspace.Workspace == _currentContextService.Workspace;
        }
    }
}