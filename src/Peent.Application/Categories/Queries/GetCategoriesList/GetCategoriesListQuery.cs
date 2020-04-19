using System.Collections.Generic;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;
using Peent.Application.Infrastructure;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<PagedResult<CategoryModel>>,
        IHavePagination, IHaveFilters, IHaveSorts
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortDto> Sort { get; } = new List<SortDto>();
        public IList<FilterDto> Filters { get; } = new List<FilterDto>();
    }
}
