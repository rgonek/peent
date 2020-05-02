using FluentValidation.Validators;

namespace Peent.Application.Common.Validators
{
    public class ExistsInCurrentContextValidatorProvider : IExistsInCurrentContextValidatorProvider
    {
        private readonly IApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;

        public ExistsInCurrentContextValidatorProvider(IApplicationDbContext db, IUserAccessor userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public IPropertyValidator In<TEntity>()
            where TEntity : class
            => new ExistsInCurrentContextValidator<TEntity>(_db, _userAccessor);
    }
}