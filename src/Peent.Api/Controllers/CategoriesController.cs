using System.Collections.Generic;
using System.Threading.Tasks;
using HybridModelBinding;
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
        [ProducesResponseType(typeof(List<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetCategoriesListQuery query)
        {
            return await Execute(query);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetCategoryQuery query)
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
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromHybrid] EditCategoryCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] DeleteCategoryCommand command)
        {
            return await Execute(command);
        }
    }
}