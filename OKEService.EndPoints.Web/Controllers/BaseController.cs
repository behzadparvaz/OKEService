using Microsoft.AspNetCore.Mvc;
using OKEService.Core.ApplicationServices.Commands;
using OKEService.Core.ApplicationServices.Queries;
using OKEService.Core.Domain.Common;
using OKEService.Core.Domain.Events;
using OKEService.EndPoints.Web.Extentions;
using OKEService.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.EndPoints.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ICommandDispatcher CommandDispatcher => HttpContext.CommandDispatcher();
        protected IQueryDispatcher QueryDispatcher => HttpContext.QueryDispatcher();
        protected IEventDispatcher EventDispatcher => HttpContext.EventDispatcher();
        protected OKEServiceServices ZaminApplicationContext => HttpContext.OKEServiceApplicationContext();




        protected async Task<IActionResult> Create<TCommand, TCommandResult>(TCommand command) where TCommand : class, ICommand<TCommandResult>
        {
            var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
            if (result.Status == ApplicationServiceStatus.Ok)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }
            return BadRequest(result.Messages);
        }

        protected async Task<IActionResult> Create<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var result = await CommandDispatcher.Send(command);
            if (result.Status == ApplicationServiceStatus.Ok)
            {
                return StatusCode((int)HttpStatusCode.Created);
            }
            return BadRequest(result.Messages);
        }

        protected async Task<IActionResult> Edit<TCommand, TCommandResult>(TCommand command) where TCommand : class, ICommand<TCommandResult>
        {
            var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
            if (result.Status == ApplicationServiceStatus.Ok)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }
            else if (result.Status == ApplicationServiceStatus.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound, command);
            }
            return BadRequest(result.Messages);
        }

        protected async Task<IActionResult> Edit<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var result = await CommandDispatcher.Send(command);
            if (result.Status == ApplicationServiceStatus.Ok)
            {
                return StatusCode((int)HttpStatusCode.Created);
            }
            else if (result.Status == ApplicationServiceStatus.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound, command);
            }
            return BadRequest(result.Messages);
        }


        protected async Task<IActionResult> Delete<TCommand, TCommandResult>(TCommand command) where TCommand : class, ICommand<TCommandResult>
        {
            var result = await CommandDispatcher.Send<TCommand, TCommandResult>(command);
            if (result.Status == ApplicationServiceStatus.Ok)
            {
                return StatusCode((int)HttpStatusCode.Created, result.Data);
            }
            else if (result.Status == ApplicationServiceStatus.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound, command);
            }
            return BadRequest(result.Messages);
        }

        protected async Task<IActionResult> Delete<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var result = await CommandDispatcher.Send(command);
            if (result.Status == ApplicationServiceStatus.Ok)
            {
                return StatusCode((int)HttpStatusCode.Created);
            }
            else if (result.Status == ApplicationServiceStatus.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound, command);
            }
            return BadRequest(result.Messages);
        }


        protected async Task<IActionResult> Query<TQuery, TQueryResult>(TQuery query) where TQuery : class, IQuery<TQueryResult>
        {
            var result = await QueryDispatcher.Execute<TQuery, TQueryResult>(query);
            if (result.Status == ApplicationServiceStatus.NotFound || result.Data == null)
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }
            else if (result.Status == ApplicationServiceStatus.Ok)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Messages);
        }
    }
}
