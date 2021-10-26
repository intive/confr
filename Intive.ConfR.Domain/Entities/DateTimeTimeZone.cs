using System;

namespace Intive.ConfR.Domain.Entities
{
    public class DateTimeTimeZone
    {
        public DateTime DateTime { get; set; }
        public string TimeZone { get; set; } = TimeZoneInfo.Utc.Id;
    }
}
