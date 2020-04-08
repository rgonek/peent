using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Peent.Application.Common;
using Peent.Application.Infrastructure;
using Peent.Common;

namespace Peent.Api.Infrastructure.ModelBinders
{
    public class SortsInfoModelBinder : IModelBinder
    {
        public const string SortQueryParameter = "sort";
        public const char DescendingPrefix = '-';
        public const char SortFieldsSeparator = ',';

        private readonly IModelBinder _worker;

        public SortsInfoModelBinder(IModelBinder worker)
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

            var sortContainer = bindingContext.Result.Model as IHaveSortsInfo;
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
                var sortInfos = new List<SortInfo>();
                foreach (var sortKey in sortKeys)
                {
                    var sortDirection = sortKey.StartsWith(DescendingPrefix) ? SortDirection.Desc : SortDirection.Asc;
                    var sortField = sortKey.TrimStart(DescendingPrefix);

                    sortInfos.Add(new SortInfo
                    {
                        Field = sortField,
                        Direction = sortDirection
                    });
                }

                var allowedFields = GetAllowedFields(bindingContext.Result.Model);

                var invalidKeys = sortInfos.Select(x => x.Field)
                    .Except(allowedFields)
                    .ToList();
                if (invalidKeys.Any())
                {
                    throw new NotAllowedSortFieldsException(invalidKeys);
                }

                foreach (var sortInfo in sortInfos)
                {
                    sortContainer.Sort.Add(sortInfo);
                }
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
