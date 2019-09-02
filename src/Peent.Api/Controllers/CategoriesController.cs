using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Categories.Commands.EditCategory;
using Peent.Application.Categories.Models;
using Peent.Application.Categories.Queries.GetCategoriesList;
using Peent.Application.Categories.Queries.GetCategory;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return await Execute(new GetCategoriesListQuery());
        }

        [HttpGet]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetCategoryQuery query)
        {
            return await Execute(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            return await Execute(command);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromBody] EditCategoryCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand command)
        {
            return await Execute(command);
        }
    }
}