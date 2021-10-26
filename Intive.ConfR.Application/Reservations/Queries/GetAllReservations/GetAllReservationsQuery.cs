using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQuery : IRequest<GetAllReservationsModel>
    {
        [Required]
        public string Mail { get; set; }
        public DateTime Start { get; set; }
        public int? Count { get; set; }
    }
}
