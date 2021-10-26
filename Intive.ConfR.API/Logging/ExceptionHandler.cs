using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Intive.ConfR.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Intive.ConfR.LoggingMiddleware
{
    public class LoggingMiddleware
    {
        private readonly ILoggerManager _logger;
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }
        /// <summary>
        /// Creates response body if error occured
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                await FormatResponse(context.Response);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        /// <summary>
        /// Format request to fit for <see cref="Invoke(HttpContext)"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Formatted request</returns>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
        /// <summary>
        /// Format request to fit for Swagger
        /// </summary>
        /// <param name="response"></param>
        /// <returns>Formatted response body</returns>
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class LoggingMiddlewareExtensions
    {
        /// <summary>
        /// Adds middleware for logging
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Intive.ConfR.LoggingMiddleware.LoggingMiddleware>();
        }
    }
}