using Intive.ConfR.Application.Reservations.Queries.GetReservation;
using System.Collections.Generic;

namespace Intive.ConfR.Application.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsModel
    {
        public IList<GetReservationModel> Value { get; set; }
    }
}
