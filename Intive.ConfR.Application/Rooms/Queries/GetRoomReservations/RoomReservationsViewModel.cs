using Intive.ConfR.Domain.Entities;
using System.Collections.Generic;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomReservations
{
    public class RoomReservationsViewModel
    {
        public IList<Reservation> Reservations { get; set; }
    }
}
