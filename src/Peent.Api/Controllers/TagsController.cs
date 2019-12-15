using System.Collections.Generic;
using System.Threading.Tasks;
using HybridModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Tags.Commands.CreateTag;
using Peent.Application.Tags.Commands.DeleteTag;
using Peent.Application.Tags.Commands.EditTag;
using Peent.Application.Tags.Models;
using Peent.Application.Tags.Queries.GetTag;
using Peent.Application.Tags.Queries.GetTagsList;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : BaseController
    {
        [HttpPost]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<TagModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromBody] GetTagsListQuery query)
        {
            return await Execute(query);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(TagModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetTagQuery query)
        {
            return await Execute(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateTagCommand command)
        {
            return await Execute(command);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromHybrid] EditTagCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] DeleteTagCommand command)
        {
            return await Execute(command);
        }
    }
}