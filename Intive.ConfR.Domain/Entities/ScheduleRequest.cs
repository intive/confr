using System.Collections.Generic;

namespace Intive.ConfR.Domain.Entities
{
    public class ScheduleRequest
    {
        public List<string> Schedules { get; set; }
        public DateTimeTimeZone StartTime { get; set; }
        public DateTimeTimeZone EndTime { get; set; }
        public int AvailabilityViewInterval { get; set; } = 30;
    }
}
