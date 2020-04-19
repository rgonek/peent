using System;
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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (typeof(IHaveSortsInfo).IsAssignableFrom(context.Metadata.ModelType))
            {
                return new SortsInfoModelBinder(_workerProvider.GetBinder(context));
            }

            return null;
        }
    }
}
