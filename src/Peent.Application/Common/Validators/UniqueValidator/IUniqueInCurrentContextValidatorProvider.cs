namespace Peent.Application.Common.Validators.UniqueValidator
{
    public interface IUniqueInCurrentContextValidatorProvider
    {
        IUniquePredicate<TEntity> In<TEntity>() where TEntity : class;
    }
}