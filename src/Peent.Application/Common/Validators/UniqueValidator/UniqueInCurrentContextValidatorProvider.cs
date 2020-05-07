namespace Peent.Application.Common.Validators.UniqueValidator
{
    public class UniqueInCurrentContextValidatorProvider : IUniqueInCurrentContextValidatorProvider
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentContextService _userAccessor;

        public UniqueInCurrentContextValidatorProvider(IApplicationDbContext db, ICurrentContextService userAccessor)
            => (_db, _userAccessor) = (db, userAccessor);

        public IUniquePredicate<TEntity> In<TEntity>()
            where TEntity : class
            => new UniquePredicate<TEntity>(_db, _userAccessor);
    }
}