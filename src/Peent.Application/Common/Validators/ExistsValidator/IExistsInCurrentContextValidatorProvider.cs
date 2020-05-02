using FluentValidation.Validators;

namespace Peent.Application.Common.Validators.ExistsValidator
{
    public interface IExistsInCurrentContextValidatorProvider
    {
        IPropertyValidator In<TEntity>() where TEntity : class;
    }
}