using FluentValidation.Validators;

namespace Peent.Application.Common.Validators
{
    public interface IExistsInCurrentContextValidatorProvider
    {
        IPropertyValidator In<TEntity>() where TEntity : class;
    }
}