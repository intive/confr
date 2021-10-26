using Intive.ConfR.Domain.ValueObjects;
using System;

namespace Intive.ConfR.Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public EMailAddress RoomEmail { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public string UserDisplayName { get; set; }
        public Guid UserId { get; set; }
    }
}
