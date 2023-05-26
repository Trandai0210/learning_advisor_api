using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Models
{
    public class JWTInHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        public JWTInHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var name = "JwtCookie";
            var cookie = context.Request.Cookies[name];

            if (cookie != null)
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                    context.Request.Headers.Append("Authorization", "Bearer " + cookie);
            }
            await _next.Invoke(context);
        }
    }
}
