using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Intive.ConfR.Infrastructure.ApiClient;
using Microsoft.AspNetCore.Http;
using Intive.ConfR.Application.Exceptions;
using System.Net;

namespace Intive.ConfR.Infrastructure
{
    public class RoomService : IRoomService
    {
        private readonly IRoomsDirectory _graphApi;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MicrosoftGraphApiClient _client;

        public RoomService(IRoomsDirectory graphApi, IHttpContextAccessor httpContextAccessor)
        {
            _graphApi = graphApi;
            _httpContextAccessor = httpContextAccessor;
            _client = new MicrosoftGraphApiClient();

        }

        public async Task<Room> GetRoomByEmail(string email)
        {
            return await _graphApi.FindRoomByEmail(email);
        }

        public async Task<IList<Room>> GetRooms()
        {
            return await _graphApi.FindRooms();
        }

        /// <summary>
        /// Returns list of rooms with details
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetRoomsBasicList()
        {
            var taskList = new List<Task<Room>>();
            var rooms = (List<Room>) await GetRooms();

            rooms.ForEach(r => taskList.Add(GetRoomByEmail(r.Email)));
            
            var result = (await Task.WhenAll(taskList)).ToList();

            return result.OrderBy(r => r.Name).ToList();
        }

        /// <summary>
        /// Return list of room schedules with availability field
        /// </summary>
        /// <param name="body">object of <see cref="ScheduleRequest"/></param>
        /// <returns></returns>
        public async Task<List<Schedule>> GetSchedulesList(ScheduleRequest body)
        {
            var request = new GraphApiPostRequest<ScheduleRequest>
            {
                GraphVersion = "beta/",
                Endpoint = $"me/calendar/getschedule",
                Body = body
            };

            var httpContext = _httpContextAccessor.HttpContext;

            var roomSchedules = await _client.Post<GraphListResponse<Schedule>, ScheduleRequest>(request, httpContext);
            var schedulesWithErrors = roomSchedules.Value.Where(sched => sched.Error != null).ToList();

            if (schedulesWithErrors.Any())
            {
                var missingMail = schedulesWithErrors.Where(sched => sched.Error.ResponseCode == "ErrorMailRecipientNotFound").ToList();

                if (missingMail.Any())
                {
                    var errorMessages = missingMail.Select(m => m.Error.Message).ToList();
                    throw new GraphApiException(HttpStatusCode.NotFound, string.Join(string.Empty, errorMessages));
                }
            }
            return roomSchedules.Value;
        }

        public bool CheckRoomsAvailability(List<Schedule> schedules)
        {
            return schedules.Any(s => s.AvailabilityView.Any(a => a != '0'));
        }

        /// <summary>
        /// Return a random room's schedule or an empty list.
        /// </summary>
        /// <param name="schedules">List of room schedule</param>
        /// <returns></returns>
        public List<Schedule> PickRandomSchedule(List<Schedule> schedules)
        {
            var rand = new Random();
            var shuffledSchedule = schedules.OrderBy(x => rand.Next()).ToList();

            var freeRoom = new List<Schedule>();

            foreach (var schedule in shuffledSchedule)
            {
                freeRoom.Add(schedule);

                if (!CheckRoomsAvailability(freeRoom))
                {
                    break;
                }
                freeRoom.Clear();
            }

            return freeRoom;
        }
    }
}
