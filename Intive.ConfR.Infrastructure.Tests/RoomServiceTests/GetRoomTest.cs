using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.Tests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Intive.ConfR.Infrastructure.Tests.RoomServiceTests
{
    [Collection("ServiceCollection")]
    public class GetRoomTest
    {
        private readonly IRoomService _roomService;
        private readonly IList<Room> _roomsList;

        public GetRoomTest(ServiceTestFixture fixture)
        {
            var mapper = fixture.Mapper;
            IAuthService authService = fixture.AuthService;
            IHttpContextAccessor httpContextAccessor = fixture.ContextAccessor;
            var graphService = new MicrosoftGraphRoomsApi(mapper, authService);
            _roomService = new RoomService(graphService, httpContextAccessor);
            _roomsList = fixture.RoomsList;
        }

        [Fact]
        public async Task ShouldReturnRoomList()
        {
            var rooms = await _roomService.GetRooms();

            Assert.IsType<List<Room>>(rooms);
            Assert.True(rooms.Count > 0);
        }

        [Fact]
        public async Task ShouldReturnMockRoomList()
        {
            var mock = new Mock<IRoomService>();

            mock.Setup(rs => rs.GetRooms()).Returns(Task.FromResult(_roomsList));

            var rooms = await mock.Object.GetRooms();

            Assert.IsType<List<Room>>(rooms);
            Assert.True(rooms.Count == 3);
        }
    }
}
