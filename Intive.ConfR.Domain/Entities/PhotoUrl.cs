using Intive.ConfR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intive.ConfR.Domain.Entities
{
    public class PhotoUrl
    {
        public Guid Id { get; set; }
        public EMailAddress RoomEmail { get; set; }
        public string Url { get; set; }
    }
}
