using MediatR;

namespace Intive.ConfR.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public string CommentId { get; set; }
    }
}
