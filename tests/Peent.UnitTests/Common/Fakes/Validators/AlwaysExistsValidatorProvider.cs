using FluentValidation.Validators;
using Peent.Application.Common.Validators;
using Peent.Application.Common.Validators.ExistsValidator;

namespace Peent.UnitTests.Common.Fakes.Validators
{
    public class AlwaysExistsValidatorProvider : IExistsInCurrentContextValidatorProvider
    {
        public IPropertyValidator In<TEntity>() where TEntity : class
            => new AlwaysTrueValidator();
    }
}