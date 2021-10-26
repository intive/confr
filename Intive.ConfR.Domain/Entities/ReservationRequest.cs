using System.Collections.Generic;

namespace Intive.ConfR.Domain.Entities
{
    public class ReservationRequest
    {
        public string Subject { get; set; }
        public ItemBody Body { get; set; }
        public DateTimeTimeZone Start { get; set; }
        public DateTimeTimeZone End { get; set; }
        public List<Location> Locations { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}
