using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Reservations.Commands.CancelReservation;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.Infrastructure
{
    public class ReservationService : IReservationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MicrosoftGraphApiClient _client;

        public ReservationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _client = new MicrosoftGraphApiClient();
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="start"></param>
        /// <returns>List of <see cref="GraphReservation"/></returns>
        public async Task<List<GraphReservation>> GetAllReservations(string mail, string start, int? count)
        {
            if (count == null)
                count = 420;

            var request = new GraphApiGetRequest()
            {
                GraphVersion = "beta/",
                Endpoint = $"users/{mail}/events?$top={count}&$filter=start/dateTime ge '{start}'"
            };

            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Request.Headers.Add("Prefer", "outlook.body-content-type=\"text\"");

            var reservationDetailsList = await _client.Get<GraphListResponse<GraphReservation>>(request, httpContext);

            return reservationDetailsList.Value;
        }

        /// <summary>
        /// Returns the list of user/room reservations with date range from-to
        /// </summary>
        /// <param name="from">Start date with time</param>
        /// <param name="to">End date with time</param>
        /// <param name="email">User/room email</param>
        /// <returns>List of <see cref="GraphReservation"/> objects</returns>
        public async Task<List<GraphReservation>> GetReservationsList(DateTime from, DateTime to, string email)
        {
            var endpoint = (email == null)
                ? "me/calendar/calendarView"
                : $"users/{email}/calendar/calendarView";

            var query = new Dictionary<string, string>
            {
                { "$top", "500"},
                { "startDateTime", from.ToString("s") },
                { "endDateTime", to.ToString("s") }
            };

            var request = new GraphApiGetRequest
            {
                GraphVersion = "beta/",
                Endpoint = endpoint,
                QueryParameters = query
            };

            var httpContext = _httpContextAccessor.HttpContext;

            var reservationsDetail = await _client.Get<GraphListResponse<GraphReservation>>(request, httpContext);

            return reservationsDetail.Value;
        }

        /// <summary>
        /// Get reservation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mail"></param>
        /// <returns>object of <see cref="GraphReservation"/></returns>
        public async Task<GraphReservation> GetReservation(string id, string mail)
        {
            var request = new GraphApiGetRequest()
            {
                GraphVersion = "beta/",
                Endpoint = $"users/{mail}/events/{id}"
            };

            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Request.Headers.Add("Prefer", "outlook.body-content-type=\"text\"");

            return await _client.Get<GraphReservation>(request, httpContext);
        }

        /// <summary>
        /// Creates new room reservation
        /// </summary>
        /// <param name="body">object of <see cref="ReservationRequest"/></param>
        /// <returns>object of <see cref="GraphReservation"/></returns>
        public async Task<GraphReservation> CreateReservation(ReservationRequest body)
        {
            var request = new GraphApiPostRequest<ReservationRequest>
            {
                GraphVersion = "beta/",
                Endpoint = "me/calendar/events",
                Body = body
            };

            var httpContext = _httpContextAccessor.HttpContext;

            return await _client.Post<GraphReservation, ReservationRequest>(request, httpContext);
        }

        /// <summary>
        /// Deletes room reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing</returns>
        public async Task DeleteReservation(string id)
        {
            var request = new GraphApiDeleteRequest()
            {
                GraphVersion = "beta/",
                Endpoint = "me/events/",
                Id = id
            };

            var httpContext = _httpContextAccessor.HttpContext;

            await _client.Delete(request, httpContext);
        }

        /// <summary>
        /// Cancel reservation attendance
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns>nothing</returns>
        public async Task CancelReservation(string id, string comment)
        {
            var request = new GraphApiPostRequest<CancelReservationCommand>
            {
                GraphVersion = "beta/",
                Endpoint = $"me/events/{id}/cancel"
            };

            var httpContext = _httpContextAccessor.HttpContext;

            await _client.Post<GraphReservation, CancelReservationCommand>(request, httpContext);
        }

        /// <summary>
        /// Returns swapped list of room reservations with user reservations
        /// </summary>
        /// <param name="userReservations">User reservations</param>
        /// <param name="roomReservations">Room reservations</param>
        /// <returns>List of <see cref="GraphReservation"/></returns>
        public List<GraphReservation> SwapReservations(List<GraphReservation> userReservations, List<GraphReservation> roomReservations)
        {
            var newReservationsList = new List<GraphReservation>();
            roomReservations.ForEach(rr =>
            {
                var reservation = userReservations.SingleOrDefault(ur => ur.Start.DateTime == rr.Start.DateTime
                                                                        && ur.End.DateTime == rr.End.DateTime);
                newReservationsList.Add(reservation ?? rr);
            });

            return newReservationsList;
        }
    }
}
