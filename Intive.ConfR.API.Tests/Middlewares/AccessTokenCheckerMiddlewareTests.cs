using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Intive.ConfR.API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace Intive.ConfR.API.Tests.Middlewares
{
    public class AccessTokenCheckerMiddlewareTests
    {
        [Fact]
        public async Task WhenAccessTokenIsEmpty()
        {

            var middleware = new AccessTokenCheckerMiddleware(new Mock<RequestDelegate>().Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            var bodyText = GetContextBody(context);

            Assert.Equal(401, context.Response.StatusCode);
            Assert.Equal("Access token is required in request header: \"Access_token: token\".", bodyText);
        }

        [Fact]
        public async Task WhenAccessTokenIsNotAdded()
        {
            var middleware = new AccessTokenCheckerMiddleware(new Mock<RequestDelegate>().Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            context.Request.Headers.Add(new KeyValuePair<string, StringValues>("Access_token", string.Empty));

            await middleware.Invoke(context);

            var bodyText = GetContextBody(context);

            Assert.Equal(401, context.Response.StatusCode);
            Assert.Equal("Access token cannot be empty.", bodyText);
        }

        [Fact]
        public async Task WhenAccessTokenIsAdded()
        {
            var middleware = new AccessTokenCheckerMiddleware(new Mock<RequestDelegate>().Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            context.Request.Headers.Add(new KeyValuePair<string, StringValues>("Access_token", "token"));

            await middleware.Invoke(context);

            var bodyText = GetContextBody(context);

            Assert.Equal(200, context.Response.StatusCode);
            Assert.Equal(string.Empty, bodyText);
        }

        private string GetContextBody(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            return reader.ReadToEnd();
        }
    }
}
