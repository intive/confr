using Intive.ConfR.Domain;
using MediatR;

namespace Intive.ConfR.Application.Comments.Queries.GetComment
{
    public class GetCommentQuery : IRequest<CommentDTO>
    {
        public string CommentId { get; set; }
    }
}
