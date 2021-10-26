using System;
using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Infrastructure;
using Xunit;

namespace Intive.ConfR.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public IMapper Mapper { get; private set; }
        public IRoomService RoomService { get; private set; }
        public AuthorizationData AuthorizationData { get; private set; }


    public QueryTestFixture()
        {
            Mapper = AutoMapperFactory.Create();
            RoomService = new MockRoomService();
            AuthorizationData = new AuthorizationData
            {
                AuthUrl = "https://login.microsoftonline.com/67cf6398-28ec-44cd-b3c0-881f597f02f3/oauth2/v2.0/token",

            };
        }

        public void Dispose()
        {
            //clear tests temps
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}