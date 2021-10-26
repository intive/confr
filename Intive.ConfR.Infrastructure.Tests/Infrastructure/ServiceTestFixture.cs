using System;
using System.Collections.Generic;
using AutoMapper;
using Intive.ConfR.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Xunit;

namespace Intive.ConfR.Infrastructure.Tests.Infrastructure
{
    public class ServiceTestFixture : IDisposable
    {
        public AuthService AuthService { get; }
        public HttpContextAccessor ContextAccessor { get; }
        public IMapper Mapper { get; }
        public IList<Room> RoomsList { get; }
        public GraphUser UserData { get;}

        public ServiceTestFixture()
        {
            AuthService = new AuthService(new OptionsWrapper<AuthorizationData>(GetAuthData()));
            ContextAccessor = new HttpContextAccessor();
            Mapper = AutoMapperFactory.Create();
            RoomsList = GraphMockData.RoomsList();
            UserData = GraphMockData.GetUserData();
        }

        private static AuthorizationData GetAuthData()
        {
            return new AuthorizationData
            {
                AuthUrl = "https://login.microsoftonline.com/67cf6398-28ec-44cd-b3c0-881f597f02f3/oauth2/v2.0/token",
                Scope = "user.read openid",

            };
        }

        public void Dispose() {}
    }

    [CollectionDefinition("ServiceCollection")]
    public class ServiceCollection : ICollectionFixture<ServiceTestFixture> { }
}
