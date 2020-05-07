using FluentValidation.Validators;

namespace Peent.Application.Common.Validators.ExistsValidator
{
    public class ExistsInCurrentContextValidatorProvider : IExistsInCurrentContextValidatorProvider
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _userAccessor;

        public ExistsInCurrentContextValidatorProvider(IApplicationDbContext db, ICurrentContextService userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public IPropertyValidator In<TEntity>()
            where TEntity : class
            => new ExistsInCurrentContextValidator<TEntity>(_db, _userAccessor);
    }
}