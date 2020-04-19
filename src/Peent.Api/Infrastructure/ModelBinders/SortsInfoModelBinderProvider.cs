using EnsureThat;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Infrastructure;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class SortsInfoModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinderProvider _workerProvider;

        public SortsInfoModelBinderProvider(IModelBinderProvider workerProvider)
        {
            _workerProvider = workerProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            return typeof(IHaveSortsInfo).IsAssignableFrom(context.Metadata.ModelType)
                ? new SortsInfoModelBinder(_workerProvider.GetBinder(context))
                : null;
        }
    }
}
