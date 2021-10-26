using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Intive.ConfR.Application.Reservations.Commands.CreateRandomReservation;
using Intive.ConfR.Application.Reservations.Commands.CreateReservation;
using Intive.ConfR.Application.Rooms.Queries.GetRoomsList;
using Intive.ConfR.API;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;


namespace Intive.ConfR.IntegrationTests
{
    public class ReservationsIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ReservationsIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        //Unauthorized Access Test
        [Theory]
        [InlineData("/api/Rooms/rick.sanchez%40patronage.onmicrosoft.com/reservations")]
        public async Task GetEndpointUnauthorizedStatusCode(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }


        //Already Reserved Test
        [Fact]
        public async Task GetAlreadyReservedError()
        {
            var authData = GetAuthorizationData();

            var auth = new AuthService(new OptionsWrapper<AuthorizationData>(authData));
            var token = await auth.GetAccessToken();

            _client.DefaultRequestHeaders.Add("Authentication", "Bearer x");
            _client.DefaultRequestHeaders.Add("App-Key", "60aa7607-c621-482a-beb3-034301d71a6d");
            _client.DefaultRequestHeaders.Add("Access_token", token);

            var body = new CreateReservationCommand
            {
                Rooms = new List<string>
                {
                    "rick.sanchez@patronage.onmicrosoft.com"
                },

                From = new DateTime(2022, 12, 25, 11, 30, 0),
                To = new DateTime(2022, 12, 25, 15, 30, 0),

                Subject = "This shouldn't exist",
                Content = "If it exists it's an error",

                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        EmailAddress = new GraphEmailAddress
                        {
                            Address =  "rick.sanchez@patronage.onmicrosoft.com",
                            Name = "Rick Sanchez"
                        },

                        Type = "required"
                    }
                }
            };

            string json = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await _client.PostAsync("/api/reservations", new StringContent(json, Encoding.UTF8, "application/json"));

            Assert.Equal("Conflict", response.StatusCode.ToString());
        }

        // Without id to delete created reservation
        //[Fact]
        public async Task CreateReservationTest()
        {
            var authData = GetAuthorizationData();

            var auth = new AuthService(new OptionsWrapper<AuthorizationData>(authData));
            var token = await auth.GetAccessToken();

            _client.DefaultRequestHeaders.Add("Authentication", "Bearer x");
            _client.DefaultRequestHeaders.Add("app-key", "60aa7607-c621-482a-beb3-034301d71a6d");
            _client.DefaultRequestHeaders.Add("access_token", token);

            var response = await _client.GetAsync("/api/rooms");

            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<RoomsListViewModel>(output);

            Assert.Contains(rooms.Rooms, r => string.Equals(r.Email, "black@patronage.onmicrosoft.com"));
            
            var start = DateTime.Now.AddMonths(4);
            var end = start.AddMinutes(30);

            var body = new CreateReservationCommand
            {
                Rooms = new List<string>
                {
                    "black@patronage.onmicrosoft.com"
                },

                From = start,
                To = end,

                Subject = "Integration Tests!",
                Content = "Some integration content!",

                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        EmailAddress = new GraphEmailAddress
                        {
                            Address = "black@patronage.onmicrosoft.com",
                            Name = "Black"
                        },

                        Type = "resource"
                    }
                }
            };

            var json = JsonConvert.SerializeObject(body);

            var createResponse = await _client.PostAsync("/api/reservations", new StringContent(json, Encoding.UTF8, "application/json"));

            createResponse.EnsureSuccessStatusCode();

            var id = "type there any existing id form graph explorer"; //TODO make it id from new reservation
            var deleteResponse = await _client.DeleteAsync($"/api/reservations/{id}");

            Assert.True(deleteResponse.IsSuccessStatusCode);
        }

        // Without id to delete created reservation
        //[Fact]
        public async Task MakeRandomReservationTest()
        {
            var authData = GetAuthorizationData();

            var auth = new AuthService(new OptionsWrapper<AuthorizationData>(authData));
            var token = await auth.GetAccessToken();

            _client.DefaultRequestHeaders.Add("Authentication", "Bearer x");
            _client.DefaultRequestHeaders.Add("app-key", "60aa7607-c621-482a-beb3-034301d71a6d");
            _client.DefaultRequestHeaders.Add("access_token", token);

            var start = DateTime.Now.AddMonths(4);
            var end = start.AddMinutes(30);

            var body = new CreateRandomReservationCommand
            {
                From =start,
                To = end,

                Subject = "Integration Tests!",
                Content = "Some integration content!",

                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        EmailAddress = new GraphEmailAddress
                        {
                            Address = "black@patronage.onmicrosoft.com",
                            Name = "Black"
                        },

                        Type = "resource"
                    }
                }
            };

            var json = JsonConvert.SerializeObject(body);

            var createResponse = await _client.PostAsync("/api/reservations/random", new StringContent(json, Encoding.UTF8, "application/json"));

            createResponse.EnsureSuccessStatusCode();

            var id = "type there any existing id form graph explorer"; //TODO make it id from new reservation
            var deleteResponse = await _client.DeleteAsync($"/api/reservations/{id}");

            Assert.True(deleteResponse.IsSuccessStatusCode);
        }

        private AuthorizationData GetAuthorizationData()
        {
            return new AuthorizationData
            {
                AuthUrl = "https://login.microsoftonline.com/67cf6398-28ec-44cd-b3c0-881f597f02f3/oauth2/v2.0/token",

            };
        }
    }
}
