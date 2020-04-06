using System.Collections.Generic;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;
using Peent.Application.Infrastructure;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<PagedResult<CategoryModel>>,
        IHavePaginationInfo, IHaveFiltersInfo, IHaveSortsInfo
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortInfo> Sort { get; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; } = new List<FilterInfo>();
    }
}
