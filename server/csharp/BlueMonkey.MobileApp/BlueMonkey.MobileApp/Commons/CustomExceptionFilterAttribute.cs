using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace BlueMonkey.MobileApp.Commons
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Trace.TraceError(actionExecutedContext.Exception.ToString());
            base.OnException(actionExecutedContext);
        }
    }
}