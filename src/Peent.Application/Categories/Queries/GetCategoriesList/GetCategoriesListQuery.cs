using System.Collections.Generic;
using MediatR;
using Peent.Application.Categories.Models;
using Peent.Application.Common;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<PagedResult<CategoryModel>>
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortInfo> Sort { get; set; } = new List<SortInfo>();
        public IList<FilterInfo> Filters { get; set; } = new List<FilterInfo>();
    }
}
