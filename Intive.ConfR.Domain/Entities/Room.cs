using Intive.ConfR.Domain.ValueObjects;
using System.Collections.Generic;

namespace Intive.ConfR.Domain.Entities
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public EMailAddress Email { get; set; }
        public string Location { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int SeatsNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
