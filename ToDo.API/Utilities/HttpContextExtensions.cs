using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.API.Utilities
{
    public static class HttpContextExtensions
    {
        public static void InsertTotalItemsHeader(this HttpContext httpContext, int total)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }
             
            httpContext.Response.Headers.Add("totalItems", total.ToString());
        }
    }
}
