using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Intive.ConfR.API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Xunit;

namespace Intive.ConfR.API.Tests.Middlewares
{
    public class KeyValidatorTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("App-Key", "")]
        [InlineData("App-Key", "7a3e81f0-2f46-46b4-bc5a-e6ff5180ece3")]
        public async Task TestMultipleKeys(string header, string guid)
        {
            var config = new OptionsManager<AppKeys>(new OptionsFactory<AppKeys>(new List<IConfigureOptions<AppKeys>>(),
                new List<IPostConfigureOptions<AppKeys>>()));
            config.Value.Keys = new[] { "7a3e81f0-2f46-46b4-bc5a-e6ff5180ece3" };

            var middleware = new KeyValidatorMiddleware(s => throw new Exception("Something went wrong with invoking KeyValidatorMiddleware :("), config);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            context.Request.Headers.Add(header, guid);

            try
            {
                await middleware.Invoke(context);
                Assert.True(context.Response.StatusCode == 401);
            }
            catch (Exception ex)
            {
                Assert.Equal("Something went wrong with invoking KeyValidatorMiddleware :(", ex.Message);
            }
        }
    }
}
