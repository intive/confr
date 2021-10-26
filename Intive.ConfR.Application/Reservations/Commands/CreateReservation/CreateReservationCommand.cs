using System;
using System.Collections.Generic;
using Intive.ConfR.Domain.Entities;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest
    {
        public List<string> Rooms { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}
