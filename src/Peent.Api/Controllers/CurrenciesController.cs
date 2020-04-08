using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Currencies.Commands.CreateCurrency;
using Peent.Application.Currencies.Commands.DeleteCurrency;
using Peent.Application.Currencies.Commands.EditCurrency;
using Peent.Application.Currencies.Models;
using Peent.Application.Currencies.Queries.GetCurrency;
using Peent.Application.Currencies.Queries.GetCurrenciesList;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<CurrencyModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return await Execute(new GetCurrenciesListQuery());
        }

        [HttpGet]
        [ProducesResponseType(typeof(CurrencyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetCurrencyQuery query)
        {
            return await Execute(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyCommand command)
        {
            return await Execute(command);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromBody] EditCurrencyCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteCurrencyCommand command)
        {
            return await Execute(command);
        }
    }
}