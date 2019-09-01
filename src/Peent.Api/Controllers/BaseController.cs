using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Peent.Application.Exceptions;

namespace Peent.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected async Task<IActionResult> Execute<TResponse>(IRequest<TResponse> request)
        {
            try
            {
                var response = await Mediator.Send(request);

                return Ok(response);
            }
            catch (NotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (DuplicateException duplicateException)
            {
                return Conflict(duplicateException.Message);
            }
        }
    }
}