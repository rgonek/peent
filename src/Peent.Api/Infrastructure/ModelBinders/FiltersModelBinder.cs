﻿using System;
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
    public class FiltersModelBinder : IModelBinder
    {
        private readonly IModelBinder _worker;

        public FiltersModelBinder(IModelBinder worker)
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

            var filtersContainer = bindingContext.Result.Model as IHaveFilters;
            if (filtersContainer == null)
            {
                throw new InvalidOperationException($"Expected {bindingContext.ModelName} to have been bound by ComplexTypeModelBinder");
            }

            var filterKeys = bindingContext.HttpContext.Request.Query.Keys
                .Except(new[]
                {
                    SortsModelBinder.SortQueryParameter,
                    PaginationModelBinder.PageIndexQueryParameter,
                    PaginationModelBinder.PageSizeQueryParameter
                })
                .Select(x => x.ToLower())
                .ToList();

            if (filterKeys.Any())
            {
                ThrowsOnInvalidKeys(bindingContext, filterKeys);

                foreach (var key in filterKeys)
                {
                    var valueResult = bindingContext.ValueProvider.GetValue(key);
                    if (valueResult != ValueProviderResult.None)
                    {
                        filtersContainer.Filters.Add(new FilterDto(key, valueResult.Values));
                    }
                }
            }
        }

        private static void ThrowsOnInvalidKeys(ModelBindingContext bindingContext, IList<string> filterKeys)
        {
            if (filterKeys.Except(new[] { FilterDto.Global.ToLower() }).Any())
            {
                var allowedFields = GetAllowedFields(bindingContext.Result.Model);

                var invalidKeys = filterKeys
                    .Except(allowedFields.Union(new[] { FilterDto.Global.ToLower() }))
                    .ToList();
                if (invalidKeys.Any())
                {
                    throw new NotAllowedFilterFieldsException(invalidKeys);
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
