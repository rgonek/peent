using System.Collections.Generic;
using MediatR;
using Peent.Application.Categories.Models;

namespace Peent.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<IList<CategoryModel>>
    {
    }
}
