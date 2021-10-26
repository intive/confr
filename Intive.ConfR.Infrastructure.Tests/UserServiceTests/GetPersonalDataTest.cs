using System.Threading.Tasks;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.Tests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Intive.ConfR.Infrastructure.Tests.UserServiceTests
{
    [Collection("ServiceCollection")]
    public class GetPersonalDataTest
    {
        private readonly string _accessToken;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService _userService;
        private readonly GraphUser _userData;

        public GetPersonalDataTest(ServiceTestFixture fixture)
        {
            _accessToken = fixture.AuthService.GetAccessToken().Result;
            _accessor = fixture.ContextAccessor;
            _userService = new UserService(_accessor);
            _userData = fixture.UserData;
        }

        [Fact]
        public async Task ShouldReturnUserData()
        {
            var httpContext = new DefaultHttpContext();
            _accessor.HttpContext = httpContext;
            httpContext.Request.Headers["Access_token"] = _accessToken;

            var result = await _userService.GetPersonalData();

            Assert.IsType<GraphUser>(result);
            Assert.Equal("Rick", result.GivenName);
            Assert.Equal("rick.sanchez@patronage.onmicrosoft.com", result.Mail);
        }

        [Fact]
        public async Task ShouldReturnUnauthorized()
        {
            var httpContext = new DefaultHttpContext();
            _accessor.HttpContext = httpContext;

            var exception = await Assert.ThrowsAsync<GraphApiException>(() =>
                _userService.GetPersonalData());

            var message = "Graph error 401: CompactToken parsing failed with error code: 80049217";

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task ShouldReturnMockUserData()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(us => us.GetPersonalData()).Returns(Task.FromResult(_userData));

            var result = await mock.Object.GetPersonalData();

            Assert.IsType<GraphUser>(result);
            Assert.Equal("Rick", result.GivenName);
            Assert.Equal("rick.sanchez@patronage.onmicrosoft.com", result.Mail);
        }
    }
}
