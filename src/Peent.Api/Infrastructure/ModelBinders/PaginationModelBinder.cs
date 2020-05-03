using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Common.DynamicQuery.Contracts;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class PaginationModelBinder : IModelBinder
    {
        public const string PageIndexQueryParameter = "page";
        public const string PageSizeQueryParameter = "page_size";

        private readonly IModelBinder _worker;

        public PaginationModelBinder(IModelBinder worker)
        {
            _worker = worker;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _worker.BindModelAsync(bindingContext);
            if (!bindingContext.Result.IsModelSet)
            {
                return;
            }

            var pagingContainer = bindingContext.Result.Model as IHavePagination;
            if (pagingContainer == null)
            {
                throw new InvalidOperationException($"Expected {bindingContext.ModelName} to have been bound by ComplexTypeModelBinder");
            }

            var pageIndexValueResult = bindingContext.ValueProvider.GetValue(PageIndexQueryParameter);
            if (pageIndexValueResult != ValueProviderResult.None)
            {
                if (int.TryParse(pageIndexValueResult.FirstValue, out int pageIndex))
                {
                    pagingContainer.PageIndex = pageIndex;
                }
            }

            var pageSizeValueResult = bindingContext.ValueProvider.GetValue(PageSizeQueryParameter);
            if (pageSizeValueResult != ValueProviderResult.None)
            {
                if (int.TryParse(pageSizeValueResult.FirstValue, out int pageSize))
                {
                    pagingContainer.PageSize = pageSize;
                }
            }
        }
    }
}
