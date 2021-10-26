using System;

namespace Intive.ConfR.Domain.Entities
{
    public class GraphUser
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string UserType { get; set; }
        public string UsageLocation { get; set; }
    }
}
