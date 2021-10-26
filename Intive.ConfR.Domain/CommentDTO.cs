using System;

namespace Intive.ConfR.Domain
{
    public class CommentDTO
    {
        public string CommentId { get; set; }
        public string RoomEmail { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public string UserDisplayName { get; set; }
    }
}
