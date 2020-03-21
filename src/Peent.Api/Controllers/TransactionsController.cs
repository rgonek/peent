using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HybridModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peent.Application.Categories.Commands.CreateCategory;
using Peent.Application.Categories.Commands.DeleteCategory;
using Peent.Application.Categories.Commands.EditCategory;
using Peent.Application.Categories.Models;
using Peent.Application.Categories.Queries.GetCategory;
using Peent.Application.Transactions.Commands.CreateTransaction;
using Peent.Application.Transactions.Commands.DeleteTransaction;
using Peent.Application.Transactions.Commands.EditTransaction;
using Peent.Application.Transactions.Models;
using Peent.Application.Transactions.Queries.GetTransaction;
using Peent.Application.Transactions.Queries.GetTransactionsList;

namespace Peent.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : BaseController
    {
        [HttpPost]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<TransactionModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromBody] GetTransactionsListQuery query)
        {
            return await Execute(query);
        }

        [HttpGet]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(TransactionModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetTransactionQuery query)
        {
            return await Execute(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            return await Execute(command);
        }

        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromHybrid] EditTransactionCommand command)
        {
            return await Execute(command);
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] DeleteTransactionCommand command)
        {
            return await Execute(command);
        }
    }
}