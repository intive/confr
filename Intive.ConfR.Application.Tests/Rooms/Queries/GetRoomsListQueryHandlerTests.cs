using System.Collections.Generic;
using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Rooms.Queries.GetRoomsList;
using Intive.ConfR.Application.Tests.Infrastructure;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.Tests.Infrastructure;
using Moq;
using Xunit;

namespace Intive.ConfR.Application.Tests.Rooms.Queries
{
    [Collection("QueryCollection")]
    public class GetRoomsListQueryHandlerTests
    {
        private readonly IMapper _mapper;

        public GetRoomsListQueryHandlerTests(QueryTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetRoomsTest()
        {
            var roomService = new Mock<IRoomService>();

            roomService.Setup(rs => rs.GetRoomsBasicList())
                .Returns(Task.FromResult((List<Room>)GraphMockData.RoomsList()));

            var sut = new GetRoomsListQueryHandler(_mapper, roomService.Object);

            var result = await sut.Handle(new GetRoomsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<RoomsListViewModel>();
            result.Rooms.Count.ShouldBe(3);
        }
    }
}