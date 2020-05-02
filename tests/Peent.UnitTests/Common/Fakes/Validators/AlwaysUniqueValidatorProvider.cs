using Peent.Application.Common.Validators.UniqueValidator;

namespace Peent.UnitTests.Common.Fakes.Validators
{
    public class AlwaysUniqueValidatorProvider : IUniqueInCurrentContextValidatorProvider
    {
        public IUniquePredicate<TEntity> In<TEntity>() where TEntity : class
            => new AlwaysUniquePredicate<TEntity>();
    }
}