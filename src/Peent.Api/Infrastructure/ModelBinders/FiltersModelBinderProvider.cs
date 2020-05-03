using EnsureThat;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Common.DynamicQuery.Contracts;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class FiltersModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinderProvider _workerProvider;

        public FiltersModelBinderProvider(IModelBinderProvider workerProvider)
        {
            _workerProvider = workerProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            return typeof(IHaveFilters).IsAssignableFrom(context.Metadata.ModelType)
                ? new FiltersModelBinder(_workerProvider.GetBinder(context))
                : null;
        }
    }
}
