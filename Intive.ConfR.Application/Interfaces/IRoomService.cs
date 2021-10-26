using System;
using Intive.ConfR.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IList<Room>> GetRooms();
        Task<List<Room>> GetRoomsBasicList();
        Task<Room> GetRoomByEmail(string email);
        Task<List<Schedule>> GetSchedulesList(ScheduleRequest body);
        bool CheckRoomsAvailability(List<Schedule> schedules);
        List<Schedule> PickRandomSchedule(List<Schedule> schedules);
    }
}
