using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Common;

namespace Peent.Application.Common.Validators
{
    public class ExistInAuthenticationContextValidator : AsyncValidatorBase
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;
        private readonly Type _typeEntity;

        public ExistInAuthenticationContextValidator(Type typeEntity, IApplicationDbContext db,
            IUserAccessor userAccessor)
            : base("Entity \"{EntityName}\" ({PropertyValue}) was not found.")
        {
            _typeEntity = typeEntity;
            _db = db;
            _userAccessor = userAccessor;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            context.MessageFormatter.AppendArgument("EntityName", _typeEntity.Name);
            var entity = await _db.FindAsync(_typeEntity, new[] {context.PropertyValue}, cancellation);
            if (entity is null)
            {
                return false;
            }

            if (typeof(IHaveWorkspace).IsAssignableFrom(_typeEntity) == false)
            {
                return true;
            }

            var entityWithWorkspace = entity as IHaveWorkspace;
            await _db.Entry(entityWithWorkspace).Reference(x => x.Workspace).LoadAsync(cancellation);

            return entityWithWorkspace.Workspace == _userAccessor.User.GetWorkspace();
        }
    }

    public class ExistInAuthenticationContextValidator<TEntity> : AsyncValidatorBase, IExistsInAuthenticationContextValidator<TEntity>
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public ExistInAuthenticationContextValidator(IApplicationDbContext db, IUserAccessor userAccessor)
            : base("Entity \"{EntityName}\" ({PropertyValue}) was not found.")
        {
            _db = db;
            _userAccessor = userAccessor;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var typeEntity = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", typeEntity.Name);
            var entity = await _db.Set<TEntity>().FindAsync(new[] {context.PropertyValue}, cancellation);
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

    public interface IExistsInAuthenticationContextValidator<TEntity> : IPropertyValidator
        where TEntity : class
    {
    }
}