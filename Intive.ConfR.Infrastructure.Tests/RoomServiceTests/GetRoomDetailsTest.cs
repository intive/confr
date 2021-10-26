using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.Tests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Intive.ConfR.Infrastructure.Tests.RoomServiceTests
{
    [Collection("ServiceCollection")]
    public class GetRoomDetailsTest
    {
        private readonly IRoomService _roomService;
        private readonly IList<Room> _roomList;

        public GetRoomDetailsTest(ServiceTestFixture fixture)
        {
            var mapper = fixture.Mapper;
            IAuthService authService = fixture.AuthService;
            IHttpContextAccessor httpContextAccessor = fixture.ContextAccessor;
            var graphService = new MicrosoftGraphRoomsApi(mapper, authService);
            _roomService = new RoomService(graphService, httpContextAccessor);
            _roomList = fixture.RoomsList;
        }

        [Fact]
        public async Task ShouldReturnRoomDetails()
        {
            var room = await _roomService.GetRoomByEmail("white@patronage.onmicrosoft.com");

            Assert.IsType<Room>(room);
            Assert.Equal("White", room.Name);
            Assert.Equal("Szczecin", room.City);
        }

        [Fact]
        public async Task ShouldReturnNotFound()
        {
            var fakeEmail = "fake@patroange.onmicrosoft.com";

            var exception = await Assert.ThrowsAsync<GraphApiException>(() =>
                _roomService.GetRoomByEmail(fakeEmail));

            Assert.IsType<GraphApiException>(exception);
            Assert.Equal(StatusCodes.Status404NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnMockRoomDetails()
        {
            var mock = new Mock<IRoomService>();
            mock.Setup(r => r.GetRoomByEmail("white@patronage.onmicrosoft.com")).Returns(
                Task.FromResult(_roomList.Single(r => r.Email == "white@patronage.onmicrosoft.com")));

            var room = await mock.Object.GetRoomByEmail("white@patronage.onmicrosoft.com");

            Assert.IsType<Room>(room);
            Assert.Equal("White", room.Name);
            Assert.Equal("Szczecin", room.City);
        }
    }
}
