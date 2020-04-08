using System.Collections.Generic;
using System.Threading.Tasks;
using HybridModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Application.Accounts.Models;
using Peent.Application.Accounts.Queries.GetAccount;
using Peent.Application.Accounts.Queries.GetAccountsList;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAccountsListQuery query)
        {
            return await Execute(query);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetAccountQuery query)
        {
            return await Execute(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
        {
            return await Execute(command);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromHybrid] EditAccountCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] DeleteAccountCommand command)
        {
            return await Execute(command);
        }
    }
}