using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace BlueMonkey.MobileApp.Commons
{
    public static class ApiControllerExtensions
    {
        public static string GetSid(this ApiController self)
        {
            var p = self.User as ClaimsPrincipal;
            return p?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}