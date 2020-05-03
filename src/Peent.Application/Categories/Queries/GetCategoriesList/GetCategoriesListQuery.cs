using System.Collections.Generic;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;
using Peent.Application.Common.DynamicQuery.Contracts;
using Peent.Domain.Entities;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<PagedResult<CategoryModel>>,
        IHavePagination, IHaveFilters, IHaveSorts, IHaveAllowedFields
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortDto> Sorts { get; } = new List<SortDto>();
        public IList<FilterDto> Filters { get; } = new List<FilterDto>();

        public IEnumerable<string> AllowedFields => new[]
        {
            nameof(Category.Id),
            nameof(Category.Name),
            nameof(Category.Description)
        };
    }
}
