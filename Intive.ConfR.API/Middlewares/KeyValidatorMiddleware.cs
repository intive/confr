using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Intive.ConfR.API.Middlewares
{
    public class KeyValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppKeys _keys;

        public KeyValidatorMiddleware(RequestDelegate next, IOptions<AppKeys> options)
        {
            _next = next;
            _keys = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Request.Headers.Keys.Contains("App-Key"))
                {
                    httpContext.Response.StatusCode = 401;             
                    await httpContext.Response.WriteAsync("Application Key is missing");
                    return;
                }

                var check = _keys.Keys.ToList().Contains(httpContext.Request.Headers["App-Key"]);

                if (!check)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Invalid Application Key");
                    return;
                }

                await _next.Invoke(httpContext);
            }
            catch
            {
                throw new Exception("Something went wrong with invoking KeyValidatorMiddleware :(");
            }
        }
    }

    public class AppKeys
    {
        public string[] Keys { get; set; }
    }
}
