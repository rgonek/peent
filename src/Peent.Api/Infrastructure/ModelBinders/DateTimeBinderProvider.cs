using System;
using EnsureThat;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class DateTimeBinderProvider : IMetadataDetailsProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            return context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?)
                ? new BinderTypeModelBinder(typeof(DateTimeBinder))
                : null;
        }
    }
}
