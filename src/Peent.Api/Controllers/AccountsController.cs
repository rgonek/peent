using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Accounts.Commands.CreateAccount;
using Peent.Application.Accounts.Commands.DeleteAccount;
using Peent.Application.Accounts.Commands.EditAccount;
using Peent.Application.Accounts.Models;
using Peent.Application.Accounts.Queries.GetAccountsList;
using Peent.Application.Accounts.Queries.GetAccount;
using Peent.Application.Accounts.Queries.GetAccountsListByAccountTypes;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return await Execute(new GetAccountsListQuery());
        }

        [HttpGet]
        [Route("GetAllByTypes")]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByTypes([FromQuery] GetAccountsListByAccountTypesQuery query)
        {
            return await Execute(query);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetAccountQuery query)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromBody] EditAccountCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteAccountCommand command)
        {
            return await Execute(command);
        }
    }
}