using System;

namespace Intive.ConfR.Persistence.Repositories.Exceptions
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException(Guid commentId)
            : base($"Comment with the given id \"{commentId}\" not found!")
        {

        }
    }
}
