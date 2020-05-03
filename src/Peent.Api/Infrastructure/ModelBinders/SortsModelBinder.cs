using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Common;
using Peent.Application.Common.DynamicQuery.Contracts;
using Peent.Common;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class SortsModelBinder : IModelBinder
    {
        public const string SortQueryParameter = "sort";
        public const char DescendingPrefix = '-';
        public const char SortFieldsSeparator = ',';

        private readonly IModelBinder _worker;

        public SortsModelBinder(IModelBinder worker)
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

            var sortContainer = bindingContext.Result.Model as IHaveSorts;
            if (sortContainer == null)
            {
                throw new InvalidOperationException($"Expected {bindingContext.ModelName} to have been bound by ComplexTypeModelBinder");
            }

            var sortValueResult = bindingContext.ValueProvider.GetValue(SortQueryParameter);
            if (sortValueResult == ValueProviderResult.None)
            {
                return;
            }

            var sortKeys = sortValueResult.FirstValue
                .Split(SortFieldsSeparator, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToList();

            if (sortKeys.Any())
            {
                var sorts = Parse(sortKeys).ToList();

                ThrowsOnInvalidKeys(bindingContext, sorts);

                foreach (var sort in sorts)
                {
                    sortContainer.Sorts.Add(sort);
                }
            }
        }

        private static IEnumerable<SortDto> Parse(IList<string> sortKeys)
        {
            foreach (var sortKey in sortKeys)
            {
                var sortDirection = sortKey.StartsWith(DescendingPrefix) ? SortDirection.Desc : SortDirection.Asc;
                var sortField = sortKey.TrimStart(DescendingPrefix);

                yield return new SortDto(sortField, sortDirection);
            }
        }

        private static void ThrowsOnInvalidKeys(ModelBindingContext bindingContext, IList<SortDto> sorts)
        {
            var allowedFields = GetAllowedFields(bindingContext.Result.Model);

            var invalidKeys = sorts.Select(x => x.Field)
                .Except(allowedFields)
                .ToList();
            if (invalidKeys.Any())
            {
                throw new NotAllowedSortFieldsException(invalidKeys);
            }
        }

        private static IEnumerable<string> GetAllowedFields(object model)
        {
            if (model is IHaveAllowedFields haveAllowedFields)
            {
                return haveAllowedFields.AllowedFields.Select(x => x.ToLower());
            }
            if (model.GetType().TryGetGenericType<IBaseRequest, IPagedResult, IEnumerable>(
                out var resultType))
            {
                return resultType
                    .GetProperties()
                    .Select(x => x.Name.ToLower());
            }

            return new List<string>();
        }
    }
}
