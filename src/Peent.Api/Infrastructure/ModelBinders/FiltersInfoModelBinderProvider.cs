using EnsureThat;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Infrastructure;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class FiltersInfoModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinderProvider _workerProvider;

        public FiltersInfoModelBinderProvider(IModelBinderProvider workerProvider)
        {
            _workerProvider = workerProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            return typeof(IHaveFiltersInfo).IsAssignableFrom(context.Metadata.ModelType)
                ? new FiltersInfoModelBinder(_workerProvider.GetBinder(context))
                : null;
        }
    }
}
