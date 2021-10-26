using Intive.ConfR.Infrastructure.ApiClient;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.Infrastructure.Tests
{
    public class Calendar
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Body
    {
        public string Name { get; set; }
    }

    public class MicrosoftGraphApiClientTest
    {
        [Fact]
        public async Task ShouldReturnUserEmail()
        {
            Task<string> accessToken = TokenGenerator.GetToken();
            accessToken.Wait();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Access_token"] = $"{accessToken.Result}";

            string expectedResponse = "rick.sanchez@patronage.onmicrosoft.com";

            var request = new GraphApiGetRequest
            {
                GraphVersion = "beta/",
                Endpoint = "users/rick.sanchez@patronage.onmicrosoft.com/mail"
            };

            var client = new MicrosoftGraphApiClient();

            var getResponse = await client.Get<Dictionary<string, string>>(request, httpContext);

            Assert.Equal(expectedResponse, getResponse["value"]);
        }

        [Fact]
        public async Task ShouldReturnCalendarName()
        {
            Task<string> accessToken = TokenGenerator.GetToken();
            accessToken.Wait();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Access_token"] = $"{accessToken.Result}";

            string expectedResponse = "postTest";

            Body bodyPost = new Body
            {
                Name = "postTest"
            };

            var postRequest = new GraphApiPostRequest<Body>
            {
                GraphVersion = "beta/",
                Endpoint = "users/rick.sanchez@patronage.onmicrosoft.com/calendars",
                Body = bodyPost
            };

            var client = new MicrosoftGraphApiClient();

            var postResponse = await client.Post<Calendar, Body>(postRequest, httpContext);

            Assert.Equal(expectedResponse, postResponse.Name);


            Body bodyPatch = new Body
            {
                Name = "patchTest"
            };

            var patchRequest = new GraphApiPatchRequest<Body>
            {
                GraphVersion = "beta/",
                Endpoint = "users/rick.sanchez@patronage.onmicrosoft.com/calendars",
                Id = postResponse.Id,
                Body = bodyPatch
            };

            var patchResponse = await client.Patch<Calendar, Body>(patchRequest, httpContext);

            Assert.Equal("patchTest", patchResponse.Name);

            var deleteRequest = new GraphApiDeleteRequest()
            {
                GraphVersion = "beta/",
                Endpoint = "users/rick.sanchez@patronage.onmicrosoft.com/calendars",
                Id = postResponse.Id
            };

            await client.Delete(deleteRequest, httpContext);
        }
    }
}
