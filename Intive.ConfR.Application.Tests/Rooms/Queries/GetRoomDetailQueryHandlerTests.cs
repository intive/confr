using AutoMapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Rooms.Queries.GetRoomDetail;
using Intive.ConfR.Application.Tests.Infrastructure;
using Intive.ConfR.Infrastructure;
using Intive.ConfR.Infrastructure.Tests;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Intive.ConfR.Application.Tests.Rooms.Queries
{
    [Collection("QueryCollection")]
    public class GetRoomDetailQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly AuthorizationData _authData;

        public GetRoomDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _mapper = fixture.Mapper;
            _authData = fixture.AuthorizationData;

        }

        [Fact]
        public async Task TestGetRoomDetails()
        {
            var accessToken = TokenGenerator.GetToken();
            accessToken.Wait();

            var authService = new Mock<AuthService>(new OptionsWrapper<AuthorizationData>(_authData)).Object;
            var accessor = new HttpContextAccessor();
            var httpContext = new DefaultHttpContext();
            accessor.HttpContext = httpContext;
            httpContext.Request.Headers["Access_token"] = $"{accessToken.Result}";

            var expectedResponse = "Blue";

            var graph = new MicrosoftGraphRoomsApi(_mapper, authService);

            var request = new GetRoomDetailQueryHandler(new RoomService(graph, accessor));
            var result = await request.Handle(new GetRoomDetailQuery {Email = "blue@patronage.onmicrosoft.com"},
                new CancellationToken());

            Assert.Equal(expectedResponse, result.Name);
        }

        [Fact]
        public async Task TestRoomNotFound()
        {
            var accessToken = TokenGenerator.GetToken();
            accessToken.Wait();

            var authService = new Mock<AuthService>(new OptionsWrapper<AuthorizationData>(_authData)).Object;
            var accessor = new HttpContextAccessor();
            var httpContext = new DefaultHttpContext();
            accessor.HttpContext = httpContext;
            httpContext.Request.Headers["Access_token"] = $"{accessToken.Result}";

            var graph = new MicrosoftGraphRoomsApi(_mapper, authService);

            var request = new GetRoomDetailQueryHandler(new RoomService(graph, accessor));

            var expectedResponse = 404;

            try
            {
                await request.Handle(new GetRoomDetailQuery {Email = "not found"},
                    new CancellationToken());
            }
            catch (GraphApiException ex)
            {
                Assert.Equal(ex.StatusCode, expectedResponse);
            }
        }
    }
}
