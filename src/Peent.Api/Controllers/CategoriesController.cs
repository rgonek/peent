using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Categories.Queries.GetCategory;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCategoryQuery query)
        {
            return await Execute(query);
        }
    }
}