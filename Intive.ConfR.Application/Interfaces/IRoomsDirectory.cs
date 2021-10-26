using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Interfaces
{
    public interface IRoomsDirectory
    {
        Task<IList<Room>> FindRooms();
        Task<Room> FindRoomByEmail(string email);
        Task<bool> RoomExists(EMailAddress email);
    }
}
