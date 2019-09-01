using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Categories.Models;
using Peent.Application.Categories.Queries.GetCategory;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetCategoryQuery query)
        {
            return await Execute(query);
        }
    }
}