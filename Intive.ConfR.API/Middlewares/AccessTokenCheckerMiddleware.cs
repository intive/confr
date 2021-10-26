using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.API.Middlewares
{
    public class AccessTokenCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessTokenCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!context.Request.Headers.Keys.Contains("Access_token"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access token is required in request header: \"Access_token: token\".");

                return;
            }

            if (string.IsNullOrWhiteSpace(context.Request.Headers["Access_token"]))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access token cannot be empty.");

                return;
            }

            await _next.Invoke(context);
        }
    }
}
