using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.Tests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Intive.ConfR.Infrastructure.Tests.ReservationServiceTests
{
    [Collection("ServiceCollection")]
    public class CreateReservationTest
    {
        private readonly string _accessToken;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReservationService _reservationService;

        public CreateReservationTest(ServiceTestFixture fixture)
        {
            _accessToken = fixture.AuthService.GetAccessToken().Result;
            _httpContextAccessor = fixture.ContextAccessor;
            _reservationService = new ReservationService(_httpContextAccessor);
        }

        [Fact]
        public async Task ShouldThrowUnauthorized()
        {
            var httpContext = new DefaultHttpContext();
            _httpContextAccessor.HttpContext = httpContext;

            var body = new ReservationRequest();
            
            var exception = await Assert.ThrowsAsync<GraphApiException>(() => 
                _reservationService.CreateReservation(body));

            var message = "Graph error 401: CompactToken parsing failed with error code: 80049217";

            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task ShouldThrowBadRequest()
        {
            var httpContext = new DefaultHttpContext();
            _httpContextAccessor.HttpContext = httpContext;
            httpContext.Request.Headers["Access_token"] = _accessToken;

            var date = DateTime.Now.AddYears(1).AddDays(1);

            var body = new ReservationRequest
            {
                Subject = "Test",
                Body = new ItemBody { Content = "Body test"},
                Start = new DateTimeTimeZone { DateTime = date},
                End = new DateTimeTimeZone { },
                Locations = new List<Location> { new Location { DisplayName = "White", LocationUri = "white@patronage.onmicrosoft.com"} },
                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        Type = "resource",
                        EmailAddress = new GraphEmailAddress
                            {Name = "White", Address = "white@patronage.onmicrosoft.com"}
                    }
                }
            };

            var exception = await Assert.ThrowsAsync<GraphApiException>(() =>
                _reservationService.CreateReservation(body));

            var expectedMessage = "Graph error 400: At least one property failed validation.";

            Assert.Equal(StatusCodes.Status400BadRequest, exception.StatusCode);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task ShouldCreateReservation()
        {
            var httpContext = new DefaultHttpContext();
            _httpContextAccessor.HttpContext = httpContext;
            httpContext.Request.Headers["Access_token"] = _accessToken;

            var date = DateTime.Now.AddMonths(4);

            var body = new ReservationRequest
            {
                Subject = "UTest reservation",
                Body = new ItemBody { Content = "Body test"},
                Start = new DateTimeTimeZone { DateTime = date},
                End = new DateTimeTimeZone { DateTime = date.AddHours(1)},
                Locations = new List<Location> { new Location { DisplayName = "White", LocationUri = "white@patronage.onmicrosoft.com" } },
                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        Type = "resource",
                        EmailAddress = new GraphEmailAddress
                            {Name = "White", Address = "white@patronage.onmicrosoft.com"}
                    }
                }
            };

            var result =  await _reservationService.CreateReservation(body);
            
            Assert.IsType<GraphReservation>(result);
            Assert.Equal("UTest reservation", result.Subject);
            
            await _reservationService.DeleteReservation(result.Id);
        }
    }
}
