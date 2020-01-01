using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HybridModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : BaseController
    {
        //[HttpPost]
        //[Route("GetAll")]
        //[ProducesResponseType(typeof(List<CategoryModel>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAll([FromBody] GetCategoriesListQuery query)
        //{
        //    return await Execute(query);
        //}

        //[HttpGet]
        //[Route("{id:int}")]
        //[ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Get([FromRoute] GetCategoryQuery query)
        //{
        //    return await Execute(query);
        //}

        //[HttpPost]
        //[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        //{
        //    return await Execute(command);
        //}

        //[HttpPut]
        //[Route("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //public async Task<IActionResult> Edit([FromHybrid] EditCategoryCommand command)
        //{
        //    return await Execute(command);
        //}

        //[HttpDelete]
        //[Route("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete([FromRoute] DeleteCategoryCommand command)
        //{
        //    return await Execute(command);
        //}
    }
}