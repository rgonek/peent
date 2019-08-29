using MediatR;
using Peent.Application.Categories.Models;

namespace Peent.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryModel>
    {
        public int Id { get; set; }
    }
}
