using EnsureThat;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Infrastructure;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class PaginationInfoModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinderProvider _workerProvider;

        public PaginationInfoModelBinderProvider(IModelBinderProvider workerProvider)
        {
            _workerProvider = workerProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            return typeof(IHavePaginationInfo).IsAssignableFrom(context.Metadata.ModelType)
                ? new PaginationInfoModelBinder(_workerProvider.GetBinder(context))
                : null;
        }
    }
}
