using System;
using System.Linq;
using System.Reflection;
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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (typeof(IHavePaginationInfo).IsAssignableFrom(context.Metadata.ModelType))
            {
                return new PaginationInfoModelBinder(_workerProvider.GetBinder(context));
            }

            return null;
        }
    }
}
