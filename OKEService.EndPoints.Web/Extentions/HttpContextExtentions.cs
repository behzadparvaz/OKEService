using Microsoft.AspNetCore.Http;
using OKEService.Core.ApplicationServices.Commands;
using OKEService.Core.ApplicationServices.Queries;
using OKEService.Core.Domain.Events;
using OKEService.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.EndPoints.Web.Extentions
{
    public static class HttpContextExtentions
    {
        public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
            (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
            (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
        public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
            (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher));
        public static OKEServiceServices OKEServiceApplicationContext(this HttpContext httpContext) =>
            (OKEServiceServices)httpContext.RequestServices.GetService(typeof(OKEServiceServices));
    }
}
